using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

// Shawn M. Crawford - [sleepy9090] - 2017
// reference:
// http://verhoeven272.nl/fruttenboel/Gameboy/index.html
// http://gbdev.gg8.se/wiki/articles/The_Cartridge_Header
namespace GameboyRomAnalyzer
{
    public partial class Form1 : Form
    {
        string filepath;
        bool isGameBoyROM = false;
        bool isColorGameBoyROM = false;
        bool hasColorGameBoyFeaturesEnabled = false;

        public Form1()
        {
            InitializeComponent();

            writeROMToolStripMenuItem.Enabled = false;
            fixChecksumToolStripMenuItem.Enabled = false;
            restoreNintendoLogoToolStripMenuItem.Enabled = false;
            enableColorGameBoyOptionsToolStripMenuItem.Enabled = false;

            textBoxManufacturerCode.MaxLength = 0x4;
            textBoxGameTitle.MaxLength = 0x10;
            textBoxHeaderChecksum.MaxLength = 0x2;
            textBoxCorrectHeaderChecksum.MaxLength = 0x2;
            textBoxNintendoLogo.MaxLength = 0x138; // 0x9C bytes * 2
            textBoxROMEntryPoint.MaxLength = 0x8; // 0x4 bytes * 2
            textBoxVersionNumber.MaxLength = 0x2;
            textBoxGlobalChecksum.MaxLength = 0x4;

            textBoxFileName.Enabled = false;
            textBoxManufacturerCode.Enabled = false;
            textBoxGameTitle.Enabled = false;
            textBoxHeaderChecksum.Enabled = false;
            textBoxCorrectHeaderChecksum.Enabled = false;
            textBoxNintendoLogo.Enabled = false;
            textBoxROMEntryPoint.Enabled = false;
            textBoxVersionNumber.Enabled = false;
            textBoxGlobalChecksum.Enabled = false;

            checkBoxGameBoy.Enabled = false;
            checkBoxColorGameBoy.Enabled = false;
            checkBoxIsSuperGameBoy.Enabled = false;

            populateCartridgeTypeComboBox();
            populateRAMSizeComboBox();
            populateROMSizeComboBox();
            populateDestinationCodeComboBox();
            populateComboBoxNewLicensee();
            populateComboBoxOldLicensee();

            comboBoxCartridgeType.Enabled = false;
            comboBoxRAMSize.Enabled = false;
            comboBoxROMSize.Enabled = false;
            comboBoxDestinationCode.Enabled = false;
            comboBoxOldLicensee.Enabled = false;
            comboBoxNewLicensee.Enabled = false;

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutbox = new AboutBox1();
            aboutbox.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open file...";
            openFileDialog.Filter = "gb, gbc, cgb files (*.gb,*.gbc,*.cgb)|*.gb;*.gbc;*.cgb|All files (*.*)|*.*";
            openFileDialog.Multiselect = false;

            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {

                writeROMToolStripMenuItem.Enabled = true;
                fixChecksumToolStripMenuItem.Enabled = true;
                restoreNintendoLogoToolStripMenuItem.Enabled = true;
                enableColorGameBoyOptionsToolStripMenuItem.Enabled = true;

                textBoxFileName.Enabled = true;
                textBoxManufacturerCode.Enabled = true;
                textBoxGameTitle.Enabled = true;
                textBoxHeaderChecksum.Enabled = true;
                textBoxCorrectHeaderChecksum.Enabled = true;
                textBoxNintendoLogo.Enabled = true;
                textBoxROMEntryPoint.Enabled = true;
                textBoxVersionNumber.Enabled = true;
                textBoxGlobalChecksum.Enabled = true;

                checkBoxGameBoy.Enabled = true;
                checkBoxColorGameBoy.Enabled = true;
                checkBoxIsSuperGameBoy.Enabled = true;

                comboBoxCartridgeType.Enabled = true;
                comboBoxRAMSize.Enabled = true;
                comboBoxROMSize.Enabled = true;
                comboBoxDestinationCode.Enabled = true;
                comboBoxOldLicensee.Enabled = true;
                comboBoxNewLicensee.Enabled = true;

                textBoxFileName.Clear();
                textBoxManufacturerCode.Clear();
                textBoxGameTitle.Clear();
                textBoxHeaderChecksum.Clear();
                textBoxCorrectHeaderChecksum.Clear();
                textBoxNintendoLogo.Clear();
                textBoxROMEntryPoint.Clear();
                textBoxVersionNumber.Clear();
                textBoxGlobalChecksum.Clear();

                textBoxFileName.ReadOnly = true;
                textBoxCorrectHeaderChecksum.ReadOnly = true;

                checkBoxGameBoy.Checked = false;
                checkBoxColorGameBoy.Checked = false;
                checkBoxIsSuperGameBoy.Checked = false;

                textBoxFileName.Text = openFileDialog.FileName;

                string path = textBoxFileName.Text;
                filepath = path;
                string extension = Path.GetExtension(path);
                if(extension == ".gb")
                {
                    isGameBoyROM = true;
                    isColorGameBoyROM = false;
                    hasColorGameBoyFeaturesEnabled = false;
                    enableColorGameBoyOptionsToolStripMenuItem.Checked = false;
                }
                else if(extension == ".cgb" || extension == ".gbc")
                {
                    isColorGameBoyROM = true;
                    isGameBoyROM = false;
                    hasColorGameBoyFeaturesEnabled = true;
                    enableColorGameBoyOptionsToolStripMenuItem.Checked = true;
                }
                 
                parseROMFile(path);
            }
        }

        private void parseROMFile(string path)
        {
            GameBoyROMInfo gameBoyROMInfo = new GameBoyROMInfo(path);

            textBoxROMEntryPoint.Text = gameBoyROMInfo.getROMEntryPoint();
            textBoxNintendoLogo.Text = gameBoyROMInfo.getNintendoLogo();
            textBoxGameTitle.Text = gameBoyROMInfo.getGameTitle();
            textBoxManufacturerCode.Text = gameBoyROMInfo.getManufacturerCode();
            textBoxHeaderChecksum.Text = gameBoyROMInfo.getHeaderChecksum();
            textBoxCorrectHeaderChecksum.Text = gameBoyROMInfo.getCorrectHeaderChecksum();
            textBoxGlobalChecksum.Text = gameBoyROMInfo.getGlobalChecksum();
            textBoxVersionNumber.Text = gameBoyROMInfo.getVersionNumber();

            checkBoxIsSuperGameBoy.Checked = gameBoyROMInfo.hasSuperGameBoySupport();

            // ColorGameBoy specific
            if(isColorGameBoyROM || hasColorGameBoyFeaturesEnabled)
            {
                checkBoxColorGameBoy.Checked = gameBoyROMInfo.hasColorGameBoySupport();
                checkBoxGameBoy.Checked = gameBoyROMInfo.hasGameBoySupport();
                //enableColorGameBoyOptionsToolStripMenuItem.Checked = true;
            }
            
            if(!hasColorGameBoyFeaturesEnabled)
            {
                checkBoxColorGameBoy.Enabled = false;
                checkBoxGameBoy.Enabled = false;
            }

            string destinationCode = gameBoyROMInfo.getDestinationCode();
            setDestinationCodeComboBox(destinationCode);

            string cartridgeTypeCode = gameBoyROMInfo.getCartridgeTypeCode();
            setCartridgeTypeComboBox(cartridgeTypeCode);

            string romSizeCode = gameBoyROMInfo.getROMSizeCode();
            setRomSizeComboBox(romSizeCode);

            string ramSizeCode = gameBoyROMInfo.getRAMSizeCode();
            setRAMSizeComboBox(ramSizeCode);

            string oldLicenseeCode = gameBoyROMInfo.getOldLicenseeCode();
            setOldLicenseeComboBox(oldLicenseeCode);

            string newLicenseeCode = gameBoyROMInfo.getNewLicenseeCode();
            setNewLicenseeComboBox(newLicenseeCode);

            validateHeaderChecksum();
            validateNintendoLogo();
        }

        private void populateCartridgeTypeComboBox()
        {
            Dictionary<string, string> cartridgeTypes = new Dictionary<string, string>();

            cartridgeTypes.Add("00", "00h  ROM ONLY");
            cartridgeTypes.Add("01", "01h  ROM+MBC1");
            cartridgeTypes.Add("02", "02h  ROM+MBC1+RAM");
            cartridgeTypes.Add("03", "03h  ROM+MBC1+RAM+BATTERY");
            cartridgeTypes.Add("05", "05h  ROM+MBC2");
            cartridgeTypes.Add("06", "06h  ROM+MBC2+BATTERY");
            cartridgeTypes.Add("08", "08h  ROM+RAM");
            cartridgeTypes.Add("09", "09h  ROM+RAM+BATTERY");
            cartridgeTypes.Add("0B", "0Bh  ROM+MMM01");
            cartridgeTypes.Add("0C", "0Ch  ROM+MMM01+RAM");
            cartridgeTypes.Add("0D", "0Dh  ROM+MMM01+RAM+BATTERY");
            cartridgeTypes.Add("0F", "0Fh  ROM+MBC3+TIMER+BATTERY");
            cartridgeTypes.Add("10", "10h  ROM+MBC3+TIMER+RAM+BATTERY");
            cartridgeTypes.Add("11", "11h  ROM+MBC3");
            cartridgeTypes.Add("12", "12h  ROM+MBC3+RAM");
            cartridgeTypes.Add("13", "13h  ROM+MBC3+RAM+BATTERY");
            cartridgeTypes.Add("15", "15h  ROM+MBC4");
            cartridgeTypes.Add("16", "16h  ROM+MBC4+RAM");
            cartridgeTypes.Add("17", "17h  ROM+MBC4+RAM+BATTERY");
            cartridgeTypes.Add("19", "19h  ROM+MBC5");
            cartridgeTypes.Add("1A", "1Ah  ROM+MBC5+RAM");
            cartridgeTypes.Add("1B", "1Bh  ROM+MBC5+RAM+BATTERY");
            cartridgeTypes.Add("1C", "1Ch  ROM+MBC5+RUMBLE");
            cartridgeTypes.Add("1D", "1Dh  ROM+MBC5+RUMBLE+RAM");
            cartridgeTypes.Add("1E", "1Eh  ROM+MBC5+RUMBLE+RAM+BATTERY");
            cartridgeTypes.Add("22", "22h  ROM+MBC7+BATT");
            cartridgeTypes.Add("55", "55h  GameGenie");
            cartridgeTypes.Add("56", "56h  GameShark V3.0");
            cartridgeTypes.Add("FC", "FCh  ROM+POCKET CAMERA");
            cartridgeTypes.Add("FD", "FDh  ROM+BANDAI TAMA5");
            cartridgeTypes.Add("FE", "FEh  ROM+HuC3");
            cartridgeTypes.Add("FF", "FFh  ROM+HuC1+RAM+BATTERY");
            cartridgeTypes.Add("??", "???");

            comboBoxCartridgeType.DataSource = new BindingSource(cartridgeTypes, null);
            comboBoxCartridgeType.DisplayMember = "Value";
            comboBoxCartridgeType.ValueMember = "Key";
        }

        private void populateROMSizeComboBox()
        {
            Dictionary<string, string> romSizes = new Dictionary<string, string>();

            romSizes.Add("00", "00h -  32KByte (no ROM banking)");
            romSizes.Add("01", "01h -  64KByte (4 banks)");
            romSizes.Add("02", "02h - 128KByte (8 banks)");
            romSizes.Add("03", "03h - 256KByte (16 banks)");
            romSizes.Add("04", "04h - 512KByte (32 banks)");
            romSizes.Add("05", "05h -   1MByte (64 banks)  - only 63 banks used by MBC1");
            romSizes.Add("06", "06h -   2MByte (128 banks) - only 125 banks used by MBC1");
            romSizes.Add("07", "07h -   4MByte (256 banks)");
            romSizes.Add("52", "52h - 1.1MByte (72 banks)");
            romSizes.Add("53", "53h - 1.2MByte (80 banks)");
            romSizes.Add("54", "54h - 1.5MByte (96 banks)");
            romSizes.Add("??", "???");

            comboBoxROMSize.DataSource = new BindingSource(romSizes, null);
            comboBoxROMSize.DisplayMember = "Value";
            comboBoxROMSize.ValueMember = "Key";
        }

        private void populateRAMSizeComboBox()
        {
            Dictionary<string, string> ramSizes = new Dictionary<string, string>();

            ramSizes.Add("00", "00h - None");
            ramSizes.Add("01", "01h - 2 KBytes");
            ramSizes.Add("02", "02h - 8 Kbytes");
            ramSizes.Add("03", "03h - 32 KBytes (4 banks of 8KBytes each)");
            ramSizes.Add("04", "04h - 128 KBytes (16 banks of 8KBytes each)");
            ramSizes.Add("05", "05h - 64 KBytes (8 banks of 8KBytes each)");
            ramSizes.Add("??", "???");

            comboBoxRAMSize.DataSource = new BindingSource(ramSizes, null);
            comboBoxRAMSize.DisplayMember = "Value";
            comboBoxRAMSize.ValueMember = "Key";
        }

        private void populateDestinationCodeComboBox()
        {
            Dictionary<string, string> destinationCodes = new Dictionary<string, string>();

            destinationCodes.Add("00", "00h - Japanese");
            destinationCodes.Add("01", "01h - Non-Japanese");
            destinationCodes.Add("??", "???");

            comboBoxDestinationCode.DataSource = new BindingSource(destinationCodes, null);
            comboBoxDestinationCode.DisplayMember = "Value";
            comboBoxDestinationCode.ValueMember = "Key";
        }

        private void populateComboBoxOldLicensee()
        {
            Dictionary<string, string> oldLicensees = new Dictionary<string, string>();

            oldLicensees.Add("00", "00: None");
            oldLicensees.Add("01", "01: Nintendo");
            oldLicensees.Add("08", "08: Capcom");
            oldLicensees.Add("09", "09: Hot-b");
            oldLicensees.Add("0A", "0A: Jaleco");
            oldLicensees.Add("0B", "0B: Coconuts");
            oldLicensees.Add("0C", "0C: Elite Systems");
            oldLicensees.Add("13", "13: Electronic Arts");
            oldLicensees.Add("18", "18: Hudsonsoft");
            oldLicensees.Add("19", "19: ITC Entertainment");
            oldLicensees.Add("1A", "1A: Yanoman");
            oldLicensees.Add("1D", "1D: Clary");
            oldLicensees.Add("1F", "1F: Virgin");
            oldLicensees.Add("24", "24: PCM Complete");
            oldLicensees.Add("25", "25: San-x");
            oldLicensees.Add("28", "28: Kotobuki Systems");
            oldLicensees.Add("29", "29: Seta");
            oldLicensees.Add("30", "30: Infogrames");
            oldLicensees.Add("31", "31: Nintendo");
            oldLicensees.Add("32", "32: Bandai");
            oldLicensees.Add("33", "33: GameBoy Color - See New Licensee");
            oldLicensees.Add("34", "34: Konami");
            oldLicensees.Add("35", "35: Hector");
            oldLicensees.Add("38", "38: Capcom");
            oldLicensees.Add("39", "39: Banpresto");
            oldLicensees.Add("3C", "3C: *Entertainment i");
            oldLicensees.Add("3E", "3E: Gremlin");
            oldLicensees.Add("41", "41: Ubi Soft");
            oldLicensees.Add("42", "42: Atlus");
            oldLicensees.Add("44", "44: Malibu");
            oldLicensees.Add("46", "46: Angel");
            oldLicensees.Add("47", "47: Spectrum Holoby");
            oldLicensees.Add("49", "49: Irem");
            oldLicensees.Add("4A", "4A: Virgin");
            oldLicensees.Add("4D", "4D: Malibu");
            oldLicensees.Add("4F", "4F: U.S. Gold");
            oldLicensees.Add("50", "50: Absolute");
            oldLicensees.Add("51", "51: Acclaim");
            oldLicensees.Add("52", "52: Activision");
            oldLicensees.Add("53", "53: American Sammy");
            oldLicensees.Add("54", "54: Gametek");
            oldLicensees.Add("55", "55: Park Place");
            oldLicensees.Add("56", "56: LJN");
            oldLicensees.Add("57", "57: Matchbox");
            oldLicensees.Add("59", "59: Milton Bradley");
            oldLicensees.Add("5A", "5A: Mindscape");
            oldLicensees.Add("5B", "5B: Romstar");
            oldLicensees.Add("5C", "5C: Naxat Soft");
            oldLicensees.Add("5D", "5D: Tradewest");
            oldLicensees.Add("60", "60: Titus");
            oldLicensees.Add("61", "61: Virgin");
            oldLicensees.Add("67", "67: Ocean");
            oldLicensees.Add("69", "69: Electronic Arts");
            oldLicensees.Add("6E", "6E: Elite Systems");
            oldLicensees.Add("6F", "6F: Electro Brain");
            oldLicensees.Add("70", "70: Infogames");
            oldLicensees.Add("71", "71: Interplay");
            oldLicensees.Add("72", "72: Broderbund");
            oldLicensees.Add("73", "73: Sculptered Soft");
            oldLicensees.Add("75", "75: The Sales Curve");
            oldLicensees.Add("78", "78: T*HQ");
            oldLicensees.Add("79", "79: Accolade");
            oldLicensees.Add("7A", "7A: Triffix Entertainment");
            oldLicensees.Add("7C", "7C: Microprose");
            oldLicensees.Add("7F", "7F: Kemco");
            oldLicensees.Add("80", "80: Misawa Entertainment");
            oldLicensees.Add("83", "83: Lozc");
            oldLicensees.Add("86", "86: Tokuma Shoten Intermedia");
            oldLicensees.Add("8B", "8B: Bullet-proof Software");
            oldLicensees.Add("8C", "8C: Vic Tokai");
            oldLicensees.Add("8E", "8E: Ape");
            oldLicensees.Add("8F", "8F: I'max");
            oldLicensees.Add("91", "91: Chun Soft");
            oldLicensees.Add("92", "92: Video System");
            oldLicensees.Add("93", "93: Tsuburava");
            oldLicensees.Add("95", "95: Varie");
            oldLicensees.Add("96", "96: Yonezawa/S'pal");
            oldLicensees.Add("97", "97: Kaneko");
            oldLicensees.Add("99", "99: Arc");
            oldLicensees.Add("9A", "9A: Nihon Bussan");
            oldLicensees.Add("9B", "9B: Tecmo");
            oldLicensees.Add("9C", "9C: Imagineer");
            oldLicensees.Add("9D", "9D: Banpresto");
            oldLicensees.Add("9F", "9F: Nova");
            oldLicensees.Add("A1", "A1: Hori Electric");
            oldLicensees.Add("A2", "A2: Bandai");
            oldLicensees.Add("A4", "A4: Konami");
            oldLicensees.Add("A6", "A6: Kawada");
            oldLicensees.Add("A7", "A7: Takara");
            oldLicensees.Add("A9", "A9: Technos Japan");
            oldLicensees.Add("AA", "AA: Broderbund");
            oldLicensees.Add("AC", "AC: Toei Animation");
            oldLicensees.Add("AD", "AD: Toho");
            oldLicensees.Add("AF", "AF: Namco");
            oldLicensees.Add("B0", "B0: Acclaim");
            oldLicensees.Add("B1", "B1: Ascii or Nexoft");
            oldLicensees.Add("B2", "B2: Bandai");
            oldLicensees.Add("B4", "B4: Enix");
            oldLicensees.Add("B6", "B6: HAL");
            oldLicensees.Add("B7", "B7: SNK");
            oldLicensees.Add("B9", "B9: Pony Canyon");
            oldLicensees.Add("BA", "BA: *Culture Brain O");
            oldLicensees.Add("BB", "BB: Sunsoft");
            oldLicensees.Add("BD", "BD: Sony Imagesoft");
            oldLicensees.Add("BF", "BF: Sammy");
            oldLicensees.Add("C0", "C0: Taito");
            oldLicensees.Add("C2", "C2: Kemco");
            oldLicensees.Add("C3", "C3: Squaresoft");
            oldLicensees.Add("C4", "C4: Tokuma Shoten Intermedia");
            oldLicensees.Add("C5", "C5: Data East");
            oldLicensees.Add("C6", "C6: Tonkin House");
            oldLicensees.Add("C8", "C8: Koei");
            oldLicensees.Add("C9", "C9: UFL");
            oldLicensees.Add("CA", "CA: Ultra");
            oldLicensees.Add("CB", "CB: Vap");
            oldLicensees.Add("CC", "CC: USE");
            oldLicensees.Add("CD", "CD: Meldac");
            oldLicensees.Add("CE", "CE: *Pony Canyon or");
            oldLicensees.Add("CF", "CF: Angel");
            oldLicensees.Add("D0", "D0: Taito");
            oldLicensees.Add("D1", "D1: Sofel");
            oldLicensees.Add("D2", "D2: Quest");
            oldLicensees.Add("D3", "D3: Sigma Enterprises");
            oldLicensees.Add("D4", "D4: Ask Kodansha");
            oldLicensees.Add("D6", "D6: Naxat Soft");
            oldLicensees.Add("D7", "D7: Copya Systems");
            oldLicensees.Add("D9", "D9: Banpresto");
            oldLicensees.Add("DA", "DA: Tomy");
            oldLicensees.Add("DB", "DB: LJN");
            oldLicensees.Add("DD", "DD: NCS");
            oldLicensees.Add("DE", "DE: Human");
            oldLicensees.Add("DF", "DF: Altron");
            oldLicensees.Add("E0", "E0: Jaleco");
            oldLicensees.Add("E1", "E1: Towachiki");
            oldLicensees.Add("E2", "E2: Uutaka");
            oldLicensees.Add("E3", "E3: Varie");
            oldLicensees.Add("E5", "E5: Epoch");
            oldLicensees.Add("E7", "E7: Athena");
            oldLicensees.Add("E8", "E8: Asmik");
            oldLicensees.Add("E9", "E9: Natsume");
            oldLicensees.Add("EA", "EA: King Records");
            oldLicensees.Add("EB", "EB: Atlus");
            oldLicensees.Add("EC", "EC: Epic/Sony Records");
            oldLicensees.Add("EE", "EE: IGS");
            oldLicensees.Add("F0", "F0: A Wave");
            oldLicensees.Add("F3", "F3: Extreme Entertainment");
            oldLicensees.Add("FF", "FF: LJN");
            oldLicensees.Add("??", "???");

            comboBoxOldLicensee.DataSource = new BindingSource(oldLicensees, null);
            comboBoxOldLicensee.DisplayMember = "Value";
            comboBoxOldLicensee.ValueMember = "Key";
        }

        // Code is ASCII Value
        private void populateComboBoxNewLicensee()
        {
            Dictionary<string, string> newLicensees = new Dictionary<string, string>();

            newLicensees.Add("01", "01: Nintendo");
            newLicensees.Add("02", "02: Rocket Games");
            newLicensees.Add("08", "08: Capcom");
            newLicensees.Add("09", "09: Hot B Co.");
            newLicensees.Add("0A", "0A: Jaleco");
            newLicensees.Add("0B", "0B: Coconuts Japan");
            newLicensees.Add("0C", "0C: Coconuts Japan/G.X.Media");
            newLicensees.Add("0H", "0H: Starfish");
            newLicensees.Add("0L", "0L: Warashi Inc.");
            newLicensees.Add("0N", "0N: Nowpro");
            newLicensees.Add("0P", "0P: Game Village");
            newLicensees.Add("13", "13: Electronic Arts Japan");
            newLicensees.Add("18", "18: Hudson Soft Japan");
            newLicensees.Add("19", "19: S.C.P.");
            newLicensees.Add("1A", "1A: Yonoman");
            newLicensees.Add("1G", "1G: SMDE");
            newLicensees.Add("1P", "1P: Creatures Inc.");
            newLicensees.Add("1Q", "1Q: TDK Deep Impresion");
            newLicensees.Add("20", "20: Destination Software");
            newLicensees.Add("22", "22: VR 1 Japan");
            newLicensees.Add("25", "25: San-X");
            newLicensees.Add("28", "28: Kemco Japan");
            newLicensees.Add("29", "29: Seta");
            newLicensees.Add("2H", "2H: Ubisoft Japan");
            newLicensees.Add("2K", "2K: NEC InterChannel");
            newLicensees.Add("2L", "2L: Tam");
            newLicensees.Add("2M", "2M: Jordan");
            newLicensees.Add("2N", "2N: Smilesoft");
            newLicensees.Add("2Q", "2Q: Mediakite");
            newLicensees.Add("36", "36: Codemasters");
            newLicensees.Add("37", "37: GAGA Communications");
            newLicensees.Add("38", "38: Laguna");
            newLicensees.Add("39", "39: Telstar Fun and Games");
            newLicensees.Add("41", "41: Ubi Soft Entertainment");
            newLicensees.Add("42", "42: Sunsoft");
            newLicensees.Add("47", "47: Spectrum Holobyte");
            newLicensees.Add("49", "49: IREM");
            newLicensees.Add("4D", "4D: Malibu Games");
            newLicensees.Add("4F", "4F: Eidos/U.S. Gold");
            newLicensees.Add("4J", "4J: Fox Interactive");
            newLicensees.Add("4K", "4K: Time Warner Interactive");
            newLicensees.Add("4Q", "4Q: Disney");
            newLicensees.Add("4S", "4S: Black Pearl");
            newLicensees.Add("4X", "4X: GT Interactive");
            newLicensees.Add("4Y", "4Y: RARE");
            newLicensees.Add("4Z", "4Z: Crave Entertainment");
            newLicensees.Add("50", "50: Absolute Entertainment");
            newLicensees.Add("51", "51: Acclaim");
            newLicensees.Add("52", "52: Activision");
            newLicensees.Add("53", "53: American Sammy Corp.");
            newLicensees.Add("54", "54: Take 2 Interactive");
            newLicensees.Add("55", "55: Hi Tech");
            newLicensees.Add("56", "56: LJN LTD.");
            newLicensees.Add("58", "58: Mattel");
            newLicensees.Add("5A", "5A: Mindscape/Red Orb Ent.");
            newLicensees.Add("5C", "5C: Taxan");
            newLicensees.Add("5D", "5D: Midway");
            newLicensees.Add("5F", "5F: American Softworks");
            newLicensees.Add("5G", "5G: Majesco Sales Inc");
            newLicensees.Add("5H", "5H: 3DO");
            newLicensees.Add("5K", "5K: Hasbro");
            newLicensees.Add("5L", "5L: NewKidCo");
            newLicensees.Add("5M", "5M: Telegames");
            newLicensees.Add("5N", "5N: Metro3D");
            newLicensees.Add("5P", "5P: Vatical Entertainment");
            newLicensees.Add("5Q", "5Q: LEGO Media");
            newLicensees.Add("5S", "5S: Xicat Interactive");
            newLicensees.Add("5T", "5T: Cryo Interactive");
            newLicensees.Add("5W", "5W: Red Storm Ent./BKN Ent.");
            newLicensees.Add("5X", "5X: Microids");
            newLicensees.Add("5Z", "5Z: Conspiracy Entertainment Corp.");
            newLicensees.Add("60", "60: Titus Interactive Studios");
            newLicensees.Add("61", "61: Virgin Interactive");
            newLicensees.Add("62", "62: Maxis");
            newLicensees.Add("64", "64: LucasArts Entertainment");
            newLicensees.Add("67", "67: Ocean");
            newLicensees.Add("69", "69: Electronic Arts");
            newLicensees.Add("6E", "6E: Elite Systems Ltd.");
            newLicensees.Add("6F", "6F: Electro Brain");
            newLicensees.Add("6G", "6G: The Learning Company");
            newLicensees.Add("6H", "6H: BBC");
            newLicensees.Add("6J", "6J: Software 2000");
            newLicensees.Add("6L", "6L: BAM! Entertainment");
            newLicensees.Add("6M", "6M: Studio 3");
            newLicensees.Add("6Q", "6Q: Classified Games");
            newLicensees.Add("6S", "6S: TDK Mediactive");
            newLicensees.Add("6U", "6U: DreamCatcher");
            newLicensees.Add("6V", "6V: JoWood Productions");
            newLicensees.Add("6W", "6W: SEGA");
            newLicensees.Add("6X", "6X: Wannado Edition");
            newLicensees.Add("6Y", "6Y: LSP");
            newLicensees.Add("6Z", "6Z: ITE Media");
            newLicensees.Add("70", "70: Infogrames");
            newLicensees.Add("71", "71: Interplay");
            newLicensees.Add("72", "72: JVC Musical Industries Inc.");
            newLicensees.Add("73", "73: Parker Brothers");
            newLicensees.Add("75", "75: SCI");
            newLicensees.Add("78", "78: THQ");
            newLicensees.Add("79", "79: Accolade");
            newLicensees.Add("7A", "7A: Triffix Ent. Inc.");
            newLicensees.Add("7C", "7C: Microprose Software");
            newLicensees.Add("7D", "7D: Universal Interactive Studios");
            newLicensees.Add("7F", "7F: Kemco");
            newLicensees.Add("7G", "7G: Rage Software");
            newLicensees.Add("7H", "7H: Encore");
            newLicensees.Add("7J", "7J: Zoo");
            newLicensees.Add("7K", "7K: BVM");
            newLicensees.Add("7L", "7L: Simon & Schuster Interactive");
            newLicensees.Add("7M", "7M: Asmik Ace Entertainment Inc./AIA");
            newLicensees.Add("7N", "7N: Empire Interactive");
            newLicensees.Add("7Q", "7Q: Jester Interactive");
            newLicensees.Add("7T", "7T: Scholastic");
            newLicensees.Add("7U", "7U: Ignition Entertainment");
            newLicensees.Add("7W", "7W: Stadlbauer");
            newLicensees.Add("80", "80: Misawa");
            newLicensees.Add("83", "83: LOZC");
            newLicensees.Add("8B", "8B: Bulletproof Software");
            newLicensees.Add("8C", "8C: Vic Tokai Inc.");
            newLicensees.Add("8J", "8J: General Entertainment");
            newLicensees.Add("8N", "8N: Success");
            newLicensees.Add("8P", "8P: SEGA Japan");
            newLicensees.Add("91", "91: Chun Soft");
            newLicensees.Add("92", "92: Video System");
            newLicensees.Add("93", "93: BEC");
            newLicensees.Add("96", "96: Yonezawa/S'pal");
            newLicensees.Add("97", "97: Kaneko");
            newLicensees.Add("99", "99: Victor Interactive Software");
            newLicensees.Add("9A", "9A: Nichibutsu/Nihon Bussan");
            newLicensees.Add("9B", "9B: Tecmo");
            newLicensees.Add("9C", "9C: Imagineer");
            newLicensees.Add("9F", "9F: Nova");
            newLicensees.Add("9H", "9H: Bottom Up");
            newLicensees.Add("9L", "9L: Hasbro Japan");
            newLicensees.Add("9N", "9N: Marvelous Entertainment");
            newLicensees.Add("9P", "9P: Keynet Inc.");
            newLicensees.Add("9Q", "9Q: Hands-On Entertainment");
            newLicensees.Add("A0", "A0: Telenet");
            newLicensees.Add("A1", "A1: Hori");
            newLicensees.Add("A4", "A4: Konami");
            newLicensees.Add("A6", "A6: Kawada");
            newLicensees.Add("A7", "A7: Takara");
            newLicensees.Add("A9", "A9: Technos Japan Corp.");
            newLicensees.Add("AA", "AA: JVC");
            newLicensees.Add("AC", "AC: Toei Animation");
            newLicensees.Add("AD", "AD: Toho");
            newLicensees.Add("AF", "AF: Namco");
            newLicensees.Add("AG", "AG: Media Rings Corporation");
            newLicensees.Add("AH", "AH: J-Wing");
            newLicensees.Add("AK", "AK: KID");
            newLicensees.Add("AL", "AL: MediaFactory");
            newLicensees.Add("AP", "AP: Infogrames Hudson");
            newLicensees.Add("AQ", "AQ: Kiratto. Ludic Inc");
            newLicensees.Add("B0", "B0: Acclaim Japan");
            newLicensees.Add("B1", "B1: ASCII");
            newLicensees.Add("B2", "B2: Bandai");
            newLicensees.Add("B4", "B4: Enix");
            newLicensees.Add("B6", "B6: HAL Laboratory");
            newLicensees.Add("B7", "B7: SNK");
            newLicensees.Add("B9", "B9: Pony Canyon Hanbai");
            newLicensees.Add("BA", "BA: Culture Brain");
            newLicensees.Add("BB", "BB: Sunsoft");
            newLicensees.Add("BD", "BD: Sony Imagesoft");
            newLicensees.Add("BF", "BF: Sammy");
            newLicensees.Add("BG", "BG: Magical");
            newLicensees.Add("BJ", "BJ: Compile");
            newLicensees.Add("BL", "BL: MTO Inc.");
            newLicensees.Add("BN", "BN: Sunrise Interactive");
            newLicensees.Add("BP", "BP: Global A Entertainment");
            newLicensees.Add("BQ", "BQ: Fuuki");
            newLicensees.Add("C0", "C0: Taito");
            newLicensees.Add("C2", "C2: Kemco");
            newLicensees.Add("C3", "C3: Square Soft");
            newLicensees.Add("C5", "C5: Data East");
            newLicensees.Add("C6", "C6: Tonkin House");
            newLicensees.Add("C8", "C8: Koei");
            newLicensees.Add("CA", "CA: Konami/Palcom/Ultra");
            newLicensees.Add("CB", "CB: Vapinc/NTVIC");
            newLicensees.Add("CC", "CC: Use Co.,Ltd.");
            newLicensees.Add("CD", "CD: Meldac");
            newLicensees.Add("CE", "CE: FCI/Pony Canyon");
            newLicensees.Add("CF", "CF: Angel");
            newLicensees.Add("CM", "CM: Konami Computer Entertainment Osaka");
            newLicensees.Add("CP", "CP: Enterbrain");
            newLicensees.Add("D1", "D1: Sofel");
            newLicensees.Add("D2", "D2: Quest");
            newLicensees.Add("D3", "D3: Sigma Enterprises");
            newLicensees.Add("D4", "D4: Ask Kodansa");
            newLicensees.Add("D6", "D6: Naxat");
            newLicensees.Add("D7", "D7: Copya System");
            newLicensees.Add("D9", "D9: Banpresto");
            newLicensees.Add("DA", "DA: TOMY");
            newLicensees.Add("DB", "DB: LJN Japan");
            newLicensees.Add("DD", "DD: NCS");
            newLicensees.Add("DF", "DF: Altron Corporation");
            newLicensees.Add("DH", "DH: Gaps Inc.");
            newLicensees.Add("DN", "DN: ELF");
            newLicensees.Add("E2", "E2: Yutaka");
            newLicensees.Add("E3", "E3: Varie");
            newLicensees.Add("E5", "E5: Epoch");
            newLicensees.Add("E7", "E7: Athena");
            newLicensees.Add("E8", "E8: Asmik Ace Entertainment Inc.");
            newLicensees.Add("E9", "E9: Natsume");
            newLicensees.Add("EA", "EA: King Records");
            newLicensees.Add("EB", "EB: Atlus");
            newLicensees.Add("EC", "EC: Epic/Sony Records");
            newLicensees.Add("EE", "EE: IGS");
            newLicensees.Add("EL", "EL: Spike");
            newLicensees.Add("EM", "EM: Konami Computer Entertainment Tokyo");
            newLicensees.Add("EN", "EN: Alphadream Corporation");
            newLicensees.Add("F0", "F0: A Wave");
            newLicensees.Add("G1", "G1: PCCW");
            newLicensees.Add("G4", "G4: KiKi Co Ltd");
            newLicensees.Add("G5", "G5: Open Sesame Inc.");
            newLicensees.Add("G6", "G6: Sims");
            newLicensees.Add("G7", "G7: Broccoli");
            newLicensees.Add("G8", "G8: Avex");
            newLicensees.Add("G9", "G9: D3 Publisher");
            newLicensees.Add("GB", "GB: Konami Computer Entertainment Japan");
            newLicensees.Add("GD", "GD: Square-Enix");
            newLicensees.Add("HY", "HY: Sachen");
            newLicensees.Add("??", "???");

            comboBoxNewLicensee.DataSource = new BindingSource(newLicensees, null);
            comboBoxNewLicensee.DisplayMember = "Value";
            comboBoxNewLicensee.ValueMember = "Key";
        }

        private void setCartridgeTypeComboBox(string cartridgeTypeCode)
        {
            cartridgeTypeCode = cartridgeTypeCode.ToUpper();
            switch(cartridgeTypeCode)
            {
                case "00":
                    comboBoxCartridgeType.SelectedIndex = 0;
                    break;
                case "01":
                    comboBoxCartridgeType.SelectedIndex = 1;
                    break;
                case "02":
                    comboBoxCartridgeType.SelectedIndex = 2;
                    break;
                case "03":
                    comboBoxCartridgeType.SelectedIndex = 3;
                    break;
                case "05":
                    comboBoxCartridgeType.SelectedIndex = 4;
                    break;
                case "06":
                    comboBoxCartridgeType.SelectedIndex = 5;
                    break;
                case "08":
                    comboBoxCartridgeType.SelectedIndex = 6;
                    break;
                case "09":
                    comboBoxCartridgeType.SelectedIndex = 7;
                    break;
                case "0B":
                    comboBoxCartridgeType.SelectedIndex = 8;
                    break;
                case "0C":
                    comboBoxCartridgeType.SelectedIndex = 9;
                    break;
                case "0D":
                    comboBoxCartridgeType.SelectedIndex = 10;
                    break;
                case "0F":
                    comboBoxCartridgeType.SelectedIndex = 11;
                    break;
                case "10":
                    comboBoxCartridgeType.SelectedIndex = 12;
                    break;
                case "11":
                    comboBoxCartridgeType.SelectedIndex = 13;
                    break;
                case "12":
                    comboBoxCartridgeType.SelectedIndex = 14;
                    break;
                case "13":
                    comboBoxCartridgeType.SelectedIndex = 15;
                    break;
                case "15":
                    comboBoxCartridgeType.SelectedIndex = 16;
                    break;
                case "16":
                    comboBoxCartridgeType.SelectedIndex = 17;
                    break;
                case "17":
                    comboBoxCartridgeType.SelectedIndex = 18;
                    break;
                case "19":
                    comboBoxCartridgeType.SelectedIndex = 19;
                    break;
                case "1A":
                    comboBoxCartridgeType.SelectedIndex = 20;
                    break;
                case "1B":
                    comboBoxCartridgeType.SelectedIndex = 21;
                    break;
                case "1C":
                    comboBoxCartridgeType.SelectedIndex = 22;
                    break;
                case "1D":
                    comboBoxCartridgeType.SelectedIndex = 23;
                    break;
                case "1E":
                    comboBoxCartridgeType.SelectedIndex = 24;
                    break;
                case "22":
                    comboBoxCartridgeType.SelectedIndex = 25;
                    break;
                case "55":
                    comboBoxCartridgeType.SelectedIndex = 26;
                    break;
                case "56":
                    comboBoxCartridgeType.SelectedIndex = 27;
                    break;
                case "FC":
                    comboBoxCartridgeType.SelectedIndex = 28;
                    break;
                case "FD":
                    comboBoxCartridgeType.SelectedIndex = 29;
                    break;
                case "FE":
                    comboBoxCartridgeType.SelectedIndex = 30;
                    break;
                case "FF":
                    comboBoxCartridgeType.SelectedIndex = 31;
                    break;
                default:
                    comboBoxCartridgeType.SelectedIndex = 32;
                    break;
            }

        }

        private void setRomSizeComboBox(string romSizeCode)
        {
            romSizeCode = romSizeCode.ToUpper();
            switch(romSizeCode)
            {
                case "00":
                    comboBoxROMSize.SelectedIndex = 0;
                    break;
                case "01":
                    comboBoxROMSize.SelectedIndex = 1;
                    break;
                case "02":
                    comboBoxROMSize.SelectedIndex = 2;
                    break;
                case "03":
                    comboBoxROMSize.SelectedIndex = 3;
                    break;
                case "04":
                    comboBoxROMSize.SelectedIndex = 4;
                    break;
                case "05":
                    comboBoxROMSize.SelectedIndex = 5;
                    break;
                case "06":
                    comboBoxROMSize.SelectedIndex = 6;
                    break;
                case "07":
                    comboBoxROMSize.SelectedIndex = 7;
                    break;
                case "08":
                    comboBoxROMSize.SelectedIndex = 8;
                    break;
                case "52":
                    comboBoxROMSize.SelectedIndex = 9;
                    break;
                case "53":
                    comboBoxROMSize.SelectedIndex = 10;
                    break;
                case "54":
                    comboBoxROMSize.SelectedIndex = 11;
                    break;
                default:
                    comboBoxROMSize.SelectedIndex = 12;
                    break;
            }
        }

        private void setRAMSizeComboBox(string ramSizeCode)
        {
            ramSizeCode = ramSizeCode.ToUpper();
            switch(ramSizeCode)
            {
                case "00":
                    comboBoxRAMSize.SelectedIndex = 0;
                    break;
                case "01":
                    comboBoxRAMSize.SelectedIndex = 1;
                    break;
                case "02":
                    comboBoxRAMSize.SelectedIndex = 2;
                    break;
                case "03":
                    comboBoxRAMSize.SelectedIndex = 3;
                    break;
                case "04":
                    comboBoxRAMSize.SelectedIndex = 4;
                    break;
                case "05":
                    comboBoxRAMSize.SelectedIndex = 5;
                    break;
                default:
                    comboBoxRAMSize.SelectedIndex = 6;
                    break;
            }
        }

        private void setDestinationCodeComboBox(string destinationCode)
        {
            destinationCode = destinationCode.ToUpper();
            switch(destinationCode)
            {
                case "00":
                    comboBoxDestinationCode.SelectedIndex = 0;
                    break;
                case "01":
                    comboBoxDestinationCode.SelectedIndex = 1;
                    break;
                default:
                    comboBoxDestinationCode.SelectedIndex = 2;
                    break;
            }
        }

        private void setOldLicenseeComboBox(string oldLicenseeCode)
        {
            oldLicenseeCode = oldLicenseeCode.ToUpper();
            switch(oldLicenseeCode)
            {
                case "00":
                    comboBoxOldLicensee.SelectedIndex = 0;
                    break;
                case "01":
                    comboBoxOldLicensee.SelectedIndex = 1;
                    break;
                case "08":
                    comboBoxOldLicensee.SelectedIndex = 2;
                    break;
                case "09":
                    comboBoxOldLicensee.SelectedIndex = 3;
                    break;
                case "0A":
                    comboBoxOldLicensee.SelectedIndex = 4;
                    break;
                case "0B":
                    comboBoxOldLicensee.SelectedIndex = 5;
                    break;
                case "0C":
                    comboBoxOldLicensee.SelectedIndex = 6;
                    break;
                case "13":
                    comboBoxOldLicensee.SelectedIndex = 7;
                    break;
                case "18":
                    comboBoxOldLicensee.SelectedIndex = 8;
                    break;
                case "19":
                    comboBoxOldLicensee.SelectedIndex = 9;
                    break;
                case "1A":
                    comboBoxOldLicensee.SelectedIndex = 10;
                    break;
                case "1D":
                    comboBoxOldLicensee.SelectedIndex = 11;
                    break;
                case "1F":
                    comboBoxOldLicensee.SelectedIndex = 12;
                    break;
                case "24":
                    comboBoxOldLicensee.SelectedIndex = 13;
                    break;
                case "25":
                    comboBoxOldLicensee.SelectedIndex = 14;
                    break;
                case "28":
                    comboBoxOldLicensee.SelectedIndex = 15;
                    break;
                case "29":
                    comboBoxOldLicensee.SelectedIndex = 16;
                    break;
                case "30":
                    comboBoxOldLicensee.SelectedIndex = 17;
                    break;
                case "31":
                    comboBoxOldLicensee.SelectedIndex = 18;
                    break;
                case "32":
                    comboBoxOldLicensee.SelectedIndex = 19;
                    break;
                case "33":
                    comboBoxOldLicensee.SelectedIndex = 20;
                    break;
                case "34":
                    comboBoxOldLicensee.SelectedIndex = 21;
                    break;
                case "35":
                    comboBoxOldLicensee.SelectedIndex = 22;
                    break;
                case "38":
                    comboBoxOldLicensee.SelectedIndex = 23;
                    break;
                case "39":
                    comboBoxOldLicensee.SelectedIndex = 24;
                    break;
                case "3C":
                    comboBoxOldLicensee.SelectedIndex = 25;
                    break;
                case "3E":
                    comboBoxOldLicensee.SelectedIndex = 26;
                    break;
                case "41":
                    comboBoxOldLicensee.SelectedIndex = 27;
                    break;
                case "42":
                    comboBoxOldLicensee.SelectedIndex = 28;
                    break;
                case "44":
                    comboBoxOldLicensee.SelectedIndex = 29;
                    break;
                case "46":
                    comboBoxOldLicensee.SelectedIndex = 30;
                    break;
                case "47":
                    comboBoxOldLicensee.SelectedIndex = 31;
                    break;
                case "49":
                    comboBoxOldLicensee.SelectedIndex = 32;
                    break;
                case "4A":
                    comboBoxOldLicensee.SelectedIndex = 33;
                    break;
                case "4D":
                    comboBoxOldLicensee.SelectedIndex = 34;
                    break;
                case "4F":
                    comboBoxOldLicensee.SelectedIndex = 35;
                    break;
                case "50":
                    comboBoxOldLicensee.SelectedIndex = 36;
                    break;
                case "51":
                    comboBoxOldLicensee.SelectedIndex = 37;
                    break;
                case "52":
                    comboBoxOldLicensee.SelectedIndex = 38;
                    break;
                case "53":
                    comboBoxOldLicensee.SelectedIndex = 39;
                    break;
                case "54":
                    comboBoxOldLicensee.SelectedIndex = 40;
                    break;
                case "55":
                    comboBoxOldLicensee.SelectedIndex = 41;
                    break;
                case "56":
                    comboBoxOldLicensee.SelectedIndex = 42;
                    break;
                case "57":
                    comboBoxOldLicensee.SelectedIndex = 43;
                    break;
                case "59":
                    comboBoxOldLicensee.SelectedIndex = 44;
                    break;
                case "5A":
                    comboBoxOldLicensee.SelectedIndex = 45;
                    break;
                case "5B":
                    comboBoxOldLicensee.SelectedIndex = 46;
                    break;
                case "5C":
                    comboBoxOldLicensee.SelectedIndex = 47;
                    break;
                case "5D":
                    comboBoxOldLicensee.SelectedIndex = 48;
                    break;
                case "60":
                    comboBoxOldLicensee.SelectedIndex = 49;
                    break;
                case "61":
                    comboBoxOldLicensee.SelectedIndex = 50;
                    break;
                case "67":
                    comboBoxOldLicensee.SelectedIndex = 51;
                    break;
                case "69":
                    comboBoxOldLicensee.SelectedIndex = 52;
                    break;
                case "6E":
                    comboBoxOldLicensee.SelectedIndex = 53;
                    break;
                case "6F":
                    comboBoxOldLicensee.SelectedIndex = 54;
                    break;
                case "70":
                    comboBoxOldLicensee.SelectedIndex = 55;
                    break;
                case "71":
                    comboBoxOldLicensee.SelectedIndex = 56;
                    break;
                case "72":
                    comboBoxOldLicensee.SelectedIndex = 57;
                    break;
                case "73":
                    comboBoxOldLicensee.SelectedIndex = 58;
                    break;
                case "75":
                    comboBoxOldLicensee.SelectedIndex = 59;
                    break;
                case "78":
                    comboBoxOldLicensee.SelectedIndex = 60;
                    break;
                case "79":
                    comboBoxOldLicensee.SelectedIndex = 61;
                    break;
                case "7A":
                    comboBoxOldLicensee.SelectedIndex = 62;
                    break;
                case "7C":
                    comboBoxOldLicensee.SelectedIndex = 63;
                    break;
                case "7F":
                    comboBoxOldLicensee.SelectedIndex = 64;
                    break;
                case "80":
                    comboBoxOldLicensee.SelectedIndex = 65;
                    break;
                case "83":
                    comboBoxOldLicensee.SelectedIndex = 66;
                    break;
                case "86":
                    comboBoxOldLicensee.SelectedIndex = 67;
                    break;
                case "8B":
                    comboBoxOldLicensee.SelectedIndex = 68;
                    break;
                case "8C":
                    comboBoxOldLicensee.SelectedIndex = 69;
                    break;
                case "8E":
                    comboBoxOldLicensee.SelectedIndex = 70;
                    break;
                case "8F":
                    comboBoxOldLicensee.SelectedIndex = 71;
                    break;
                case "91":
                    comboBoxOldLicensee.SelectedIndex = 72;
                    break;
                case "92":
                    comboBoxOldLicensee.SelectedIndex = 73;
                    break;
                case "93":
                    comboBoxOldLicensee.SelectedIndex = 74;
                    break;
                case "95":
                    comboBoxOldLicensee.SelectedIndex = 75;
                    break;
                case "96":
                    comboBoxOldLicensee.SelectedIndex = 76;
                    break;
                case "97":
                    comboBoxOldLicensee.SelectedIndex = 77;
                    break;
                case "99":
                    comboBoxOldLicensee.SelectedIndex = 78;
                    break;
                case "9A":
                    comboBoxOldLicensee.SelectedIndex = 79;
                    break;
                case "9B":
                    comboBoxOldLicensee.SelectedIndex = 80;
                    break;
                case "9C":
                    comboBoxOldLicensee.SelectedIndex = 81;
                    break;
                case "9D":
                    comboBoxOldLicensee.SelectedIndex = 82;
                    break;
                case "9F":
                    comboBoxOldLicensee.SelectedIndex = 83;
                    break;
                case "A1":
                    comboBoxOldLicensee.SelectedIndex = 84;
                    break;
                case "A2":
                    comboBoxOldLicensee.SelectedIndex = 85;
                    break;
                case "A4":
                    comboBoxOldLicensee.SelectedIndex = 86;
                    break;
                case "A6":
                    comboBoxOldLicensee.SelectedIndex = 87;
                    break;
                case "A7":
                    comboBoxOldLicensee.SelectedIndex = 88;
                    break;
                case "A9":
                    comboBoxOldLicensee.SelectedIndex = 89;
                    break;
                case "AA":
                    comboBoxOldLicensee.SelectedIndex = 90;
                    break;
                case "AC":
                    comboBoxOldLicensee.SelectedIndex = 91;
                    break;
                case "AD":
                    comboBoxOldLicensee.SelectedIndex = 92;
                    break;
                case "AF":
                    comboBoxOldLicensee.SelectedIndex = 93;
                    break;
                case "B0":
                    comboBoxOldLicensee.SelectedIndex = 94;
                    break;
                case "B1":
                    comboBoxOldLicensee.SelectedIndex = 95;
                    break;
                case "B2":
                    comboBoxOldLicensee.SelectedIndex = 96;
                    break;
                case "B4":
                    comboBoxOldLicensee.SelectedIndex = 97;
                    break;
                case "B6":
                    comboBoxOldLicensee.SelectedIndex = 98;
                    break;
                case "B7":
                    comboBoxOldLicensee.SelectedIndex = 99;
                    break;
                case "B9":
                    comboBoxOldLicensee.SelectedIndex = 100;
                    break;
                case "BA":
                    comboBoxOldLicensee.SelectedIndex = 101;
                    break;
                case "BB":
                    comboBoxOldLicensee.SelectedIndex = 102;
                    break;
                case "BD":
                    comboBoxOldLicensee.SelectedIndex = 103;
                    break;
                case "BF":
                    comboBoxOldLicensee.SelectedIndex = 104;
                    break;
                case "C0":
                    comboBoxOldLicensee.SelectedIndex = 105;
                    break;
                case "C2":
                    comboBoxOldLicensee.SelectedIndex = 106;
                    break;
                case "C3":
                    comboBoxOldLicensee.SelectedIndex = 107;
                    break;
                case "C4":
                    comboBoxOldLicensee.SelectedIndex = 108;
                    break;
                case "C5":
                    comboBoxOldLicensee.SelectedIndex = 109;
                    break;
                case "C6":
                    comboBoxOldLicensee.SelectedIndex = 110;
                    break;
                case "C8":
                    comboBoxOldLicensee.SelectedIndex = 111;
                    break;
                case "C9":
                    comboBoxOldLicensee.SelectedIndex = 112;
                    break;
                case "CA":
                    comboBoxOldLicensee.SelectedIndex = 113;
                    break;
                case "CB":
                    comboBoxOldLicensee.SelectedIndex = 114;
                    break;
                case "CC":
                    comboBoxOldLicensee.SelectedIndex = 115;
                    break;
                case "CD":
                    comboBoxOldLicensee.SelectedIndex = 116;
                    break;
                case "CE":
                    comboBoxOldLicensee.SelectedIndex = 117;
                    break;
                case "CF":
                    comboBoxOldLicensee.SelectedIndex = 118;
                    break;
                case "D0":
                    comboBoxOldLicensee.SelectedIndex = 119;
                    break;
                case "D1":
                    comboBoxOldLicensee.SelectedIndex = 120;
                    break;
                case "D2":
                    comboBoxOldLicensee.SelectedIndex = 121;
                    break;
                case "D3":
                    comboBoxOldLicensee.SelectedIndex = 122;
                    break;
                case "D4":
                    comboBoxOldLicensee.SelectedIndex = 123;
                    break;
                case "D6":
                    comboBoxOldLicensee.SelectedIndex = 124;
                    break;
                case "D7":
                    comboBoxOldLicensee.SelectedIndex = 125;
                    break;
                case "D9":
                    comboBoxOldLicensee.SelectedIndex = 126;
                    break;
                case "DA":
                    comboBoxOldLicensee.SelectedIndex = 127;
                    break;
                case "DB":
                    comboBoxOldLicensee.SelectedIndex = 128;
                    break;
                case "DD":
                    comboBoxOldLicensee.SelectedIndex = 129;
                    break;
                case "DE":
                    comboBoxOldLicensee.SelectedIndex = 130;
                    break;
                case "DF":
                    comboBoxOldLicensee.SelectedIndex = 131;
                    break;
                case "E0":
                    comboBoxOldLicensee.SelectedIndex = 132;
                    break;
                case "E1":
                    comboBoxOldLicensee.SelectedIndex = 133;
                    break;
                case "E2":
                    comboBoxOldLicensee.SelectedIndex = 134;
                    break;
                case "E3":
                    comboBoxOldLicensee.SelectedIndex = 135;
                    break;
                case "E5":
                    comboBoxOldLicensee.SelectedIndex = 136;
                    break;
                case "E7":
                    comboBoxOldLicensee.SelectedIndex = 137;
                    break;
                case "E8":
                    comboBoxOldLicensee.SelectedIndex = 138;
                    break;
                case "E9":
                    comboBoxOldLicensee.SelectedIndex = 139;
                    break;
                case "EA":
                    comboBoxOldLicensee.SelectedIndex = 140;
                    break;
                case "EB":
                    comboBoxOldLicensee.SelectedIndex = 141;
                    break;
                case "EC":
                    comboBoxOldLicensee.SelectedIndex = 142;
                    break;
                case "EE":
                    comboBoxOldLicensee.SelectedIndex = 143;
                    break;
                case "F0":
                    comboBoxOldLicensee.SelectedIndex = 144;
                    break;
                case "F3":
                    comboBoxOldLicensee.SelectedIndex = 145;
                    break;
                case "FF":
                    comboBoxOldLicensee.SelectedIndex = 146;
                    break;
                default:
                    comboBoxOldLicensee.SelectedIndex = 147;
                    break;
            }
        }

        private void setNewLicenseeComboBox(string newLicenseeCode)
        {
            newLicenseeCode = newLicenseeCode.ToUpper();
            switch(newLicenseeCode)
            {
                case "01":
                    comboBoxNewLicensee.SelectedIndex = 0;
                    break;
                case "02":
                    comboBoxNewLicensee.SelectedIndex = 1;
                    break;
                case "08":
                    comboBoxNewLicensee.SelectedIndex = 2;
                    break;
                case "09":
                    comboBoxNewLicensee.SelectedIndex = 3;
                    break;
                case "0A":
                    comboBoxNewLicensee.SelectedIndex = 4;
                    break;
                case "0B":
                    comboBoxNewLicensee.SelectedIndex = 5;
                    break;
                case "0C":
                    comboBoxNewLicensee.SelectedIndex = 6;
                    break;
                case "0H":
                    comboBoxNewLicensee.SelectedIndex = 7;
                    break;
                case "0L":
                    comboBoxNewLicensee.SelectedIndex = 8;
                    break;
                case "0N":
                    comboBoxNewLicensee.SelectedIndex = 9;
                    break;
                case "0P":
                    comboBoxNewLicensee.SelectedIndex = 10;
                    break;
                case "13":
                    comboBoxNewLicensee.SelectedIndex = 11;
                    break;
                case "18":
                    comboBoxNewLicensee.SelectedIndex = 12;
                    break;
                case "19":
                    comboBoxNewLicensee.SelectedIndex = 13;
                    break;
                case "1A":
                    comboBoxNewLicensee.SelectedIndex = 14;
                    break;
                case "1G":
                    comboBoxNewLicensee.SelectedIndex = 15;
                    break;
                case "1P":
                    comboBoxNewLicensee.SelectedIndex = 16;
                    break;
                case "1Q":
                    comboBoxNewLicensee.SelectedIndex = 17;
                    break;
                case "20":
                    comboBoxNewLicensee.SelectedIndex = 18;
                    break;
                case "22":
                    comboBoxNewLicensee.SelectedIndex = 19;
                    break;
                case "25":
                    comboBoxNewLicensee.SelectedIndex = 20;
                    break;
                case "28":
                    comboBoxNewLicensee.SelectedIndex = 21;
                    break;
                case "29":
                    comboBoxNewLicensee.SelectedIndex = 22;
                    break;
                case "2H":
                    comboBoxNewLicensee.SelectedIndex = 23;
                    break;
                case "2K":
                    comboBoxNewLicensee.SelectedIndex = 24;
                    break;
                case "2L":
                    comboBoxNewLicensee.SelectedIndex = 25;
                    break;
                case "2M":
                    comboBoxNewLicensee.SelectedIndex = 26;
                    break;
                case "2N":
                    comboBoxNewLicensee.SelectedIndex = 27;
                    break;
                case "2Q":
                    comboBoxNewLicensee.SelectedIndex = 28;
                    break;
                case "36":
                    comboBoxNewLicensee.SelectedIndex = 29;
                    break;
                case "37":
                    comboBoxNewLicensee.SelectedIndex = 30;
                    break;
                case "38":
                    comboBoxNewLicensee.SelectedIndex = 31;
                    break;
                case "39":
                    comboBoxNewLicensee.SelectedIndex = 32;
                    break;
                case "41":
                    comboBoxNewLicensee.SelectedIndex = 33;
                    break;
                case "42":
                    comboBoxNewLicensee.SelectedIndex = 34;
                    break;
                case "47":
                    comboBoxNewLicensee.SelectedIndex = 35;
                    break;
                case "49":
                    comboBoxNewLicensee.SelectedIndex = 36;
                    break;
                case "4D":
                    comboBoxNewLicensee.SelectedIndex = 37;
                    break;
                case "4F":
                    comboBoxNewLicensee.SelectedIndex = 38;
                    break;
                case "4J":
                    comboBoxNewLicensee.SelectedIndex = 39;
                    break;
                case "4K":
                    comboBoxNewLicensee.SelectedIndex = 40;
                    break;
                case "4Q":
                    comboBoxNewLicensee.SelectedIndex = 41;
                    break;
                case "4S":
                    comboBoxNewLicensee.SelectedIndex = 42;
                    break;
                case "4X":
                    comboBoxNewLicensee.SelectedIndex = 43;
                    break;
                case "4Y":
                    comboBoxNewLicensee.SelectedIndex = 44;
                    break;
                case "4Z":
                    comboBoxNewLicensee.SelectedIndex = 45;
                    break;
                case "50":
                    comboBoxNewLicensee.SelectedIndex = 46;
                    break;
                case "51":
                    comboBoxNewLicensee.SelectedIndex = 47;
                    break;
                case "52":
                    comboBoxNewLicensee.SelectedIndex = 48;
                    break;
                case "53":
                    comboBoxNewLicensee.SelectedIndex = 49;
                    break;
                case "54":
                    comboBoxNewLicensee.SelectedIndex = 50;
                    break;
                case "55":
                    comboBoxNewLicensee.SelectedIndex = 51;
                    break;
                case "56":
                    comboBoxNewLicensee.SelectedIndex = 52;
                    break;
                case "58":
                    comboBoxNewLicensee.SelectedIndex = 53;
                    break;
                case "5A":
                    comboBoxNewLicensee.SelectedIndex = 54;
                    break;
                case "5C":
                    comboBoxNewLicensee.SelectedIndex = 55;
                    break;
                case "5D":
                    comboBoxNewLicensee.SelectedIndex = 56;
                    break;
                case "5F":
                    comboBoxNewLicensee.SelectedIndex = 57;
                    break;
                case "5G":
                    comboBoxNewLicensee.SelectedIndex = 58;
                    break;
                case "5H":
                    comboBoxNewLicensee.SelectedIndex = 59;
                    break;
                case "5K":
                    comboBoxNewLicensee.SelectedIndex = 60;
                    break;
                case "5L":
                    comboBoxNewLicensee.SelectedIndex = 61;
                    break;
                case "5M":
                    comboBoxNewLicensee.SelectedIndex = 62;
                    break;
                case "5N":
                    comboBoxNewLicensee.SelectedIndex = 63;
                    break;
                case "5P":
                    comboBoxNewLicensee.SelectedIndex = 64;
                    break;
                case "5Q":
                    comboBoxNewLicensee.SelectedIndex = 65;
                    break;
                case "5S":
                    comboBoxNewLicensee.SelectedIndex = 66;
                    break;
                case "5T":
                    comboBoxNewLicensee.SelectedIndex = 67;
                    break;
                case "5W":
                    comboBoxNewLicensee.SelectedIndex = 68;
                    break;
                case "5X":
                    comboBoxNewLicensee.SelectedIndex = 69;
                    break;
                case "5Z":
                    comboBoxNewLicensee.SelectedIndex = 70;
                    break;
                case "60":
                    comboBoxNewLicensee.SelectedIndex = 71;
                    break;
                case "61":
                    comboBoxNewLicensee.SelectedIndex = 72;
                    break;
                case "62":
                    comboBoxNewLicensee.SelectedIndex = 73;
                    break;
                case "64":
                    comboBoxNewLicensee.SelectedIndex = 74;
                    break;
                case "67":
                    comboBoxNewLicensee.SelectedIndex = 75;
                    break;
                case "69":
                    comboBoxNewLicensee.SelectedIndex = 76;
                    break;
                case "6E":
                    comboBoxNewLicensee.SelectedIndex = 77;
                    break;
                case "6F":
                    comboBoxNewLicensee.SelectedIndex = 78;
                    break;
                case "6G":
                    comboBoxNewLicensee.SelectedIndex = 79;
                    break;
                case "6H":
                    comboBoxNewLicensee.SelectedIndex = 80;
                    break;
                case "6J":
                    comboBoxNewLicensee.SelectedIndex = 81;
                    break;
                case "6L":
                    comboBoxNewLicensee.SelectedIndex = 82;
                    break;
                case "6M":
                    comboBoxNewLicensee.SelectedIndex = 83;
                    break;
                case "6Q":
                    comboBoxNewLicensee.SelectedIndex = 84;
                    break;
                case "6S":
                    comboBoxNewLicensee.SelectedIndex = 85;
                    break;
                case "6U":
                    comboBoxNewLicensee.SelectedIndex = 86;
                    break;
                case "6V":
                    comboBoxNewLicensee.SelectedIndex = 87;
                    break;
                case "6W":
                    comboBoxNewLicensee.SelectedIndex = 88;
                    break;
                case "6X":
                    comboBoxNewLicensee.SelectedIndex = 89;
                    break;
                case "6Y":
                    comboBoxNewLicensee.SelectedIndex = 90;
                    break;
                case "6Z":
                    comboBoxNewLicensee.SelectedIndex = 91;
                    break;
                case "70":
                    comboBoxNewLicensee.SelectedIndex = 92;
                    break;
                case "71":
                    comboBoxNewLicensee.SelectedIndex = 93;
                    break;
                case "72":
                    comboBoxNewLicensee.SelectedIndex = 94;
                    break;
                case "73":
                    comboBoxNewLicensee.SelectedIndex = 95;
                    break;
                case "75":
                    comboBoxNewLicensee.SelectedIndex = 96;
                    break;
                case "78":
                    comboBoxNewLicensee.SelectedIndex = 97;
                    break;
                case "79":
                    comboBoxNewLicensee.SelectedIndex = 98;
                    break;
                case "7A":
                    comboBoxNewLicensee.SelectedIndex = 99;
                    break;
                case "7C":
                    comboBoxNewLicensee.SelectedIndex = 100;
                    break;
                case "7D":
                    comboBoxNewLicensee.SelectedIndex = 101;
                    break;
                case "7F":
                    comboBoxNewLicensee.SelectedIndex = 102;
                    break;
                case "7G":
                    comboBoxNewLicensee.SelectedIndex = 103;
                    break;
                case "7H":
                    comboBoxNewLicensee.SelectedIndex = 104;
                    break;
                case "7J":
                    comboBoxNewLicensee.SelectedIndex = 105;
                    break;
                case "7K":
                    comboBoxNewLicensee.SelectedIndex = 106;
                    break;
                case "7L":
                    comboBoxNewLicensee.SelectedIndex = 107;
                    break;
                case "7M":
                    comboBoxNewLicensee.SelectedIndex = 108;
                    break;
                case "7N":
                    comboBoxNewLicensee.SelectedIndex = 109;
                    break;
                case "7Q":
                    comboBoxNewLicensee.SelectedIndex = 110;
                    break;
                case "7T":
                    comboBoxNewLicensee.SelectedIndex = 111;
                    break;
                case "7U":
                    comboBoxNewLicensee.SelectedIndex = 112;
                    break;
                case "7W":
                    comboBoxNewLicensee.SelectedIndex = 113;
                    break;
                case "80":
                    comboBoxNewLicensee.SelectedIndex = 114;
                    break;
                case "83":
                    comboBoxNewLicensee.SelectedIndex = 115;
                    break;
                case "8B":
                    comboBoxNewLicensee.SelectedIndex = 116;
                    break;
                case "8C":
                    comboBoxNewLicensee.SelectedIndex = 117;
                    break;
                case "8J":
                    comboBoxNewLicensee.SelectedIndex = 118;
                    break;
                case "8N":
                    comboBoxNewLicensee.SelectedIndex = 119;
                    break;
                case "8P":
                    comboBoxNewLicensee.SelectedIndex = 120;
                    break;
                case "91":
                    comboBoxNewLicensee.SelectedIndex = 121;
                    break;
                case "92":
                    comboBoxNewLicensee.SelectedIndex = 122;
                    break;
                case "93":
                    comboBoxNewLicensee.SelectedIndex = 123;
                    break;
                case "96":
                    comboBoxNewLicensee.SelectedIndex = 124;
                    break;
                case "97":
                    comboBoxNewLicensee.SelectedIndex = 125;
                    break;
                case "99":
                    comboBoxNewLicensee.SelectedIndex = 126;
                    break;
                case "9A":
                    comboBoxNewLicensee.SelectedIndex = 127;
                    break;
                case "9B":
                    comboBoxNewLicensee.SelectedIndex = 128;
                    break;
                case "9C":
                    comboBoxNewLicensee.SelectedIndex = 129;
                    break;
                case "9F":
                    comboBoxNewLicensee.SelectedIndex = 130;
                    break;
                case "9H":
                    comboBoxNewLicensee.SelectedIndex = 131;
                    break;
                case "9L":
                    comboBoxNewLicensee.SelectedIndex = 132;
                    break;
                case "9N":
                    comboBoxNewLicensee.SelectedIndex = 133;
                    break;
                case "9P":
                    comboBoxNewLicensee.SelectedIndex = 134;
                    break;
                case "9Q":
                    comboBoxNewLicensee.SelectedIndex = 135;
                    break;
                case "A0":
                    comboBoxNewLicensee.SelectedIndex = 136;
                    break;
                case "A1":
                    comboBoxNewLicensee.SelectedIndex = 137;
                    break;
                case "A4":
                    comboBoxNewLicensee.SelectedIndex = 138;
                    break;
                case "A6":
                    comboBoxNewLicensee.SelectedIndex = 139;
                    break;
                case "A7":
                    comboBoxNewLicensee.SelectedIndex = 140;
                    break;
                case "A9":
                    comboBoxNewLicensee.SelectedIndex = 141;
                    break;
                case "AA":
                    comboBoxNewLicensee.SelectedIndex = 142;
                    break;
                case "AC":
                    comboBoxNewLicensee.SelectedIndex = 143;
                    break;
                case "AD":
                    comboBoxNewLicensee.SelectedIndex = 144;
                    break;
                case "AF":
                    comboBoxNewLicensee.SelectedIndex = 145;
                    break;
                case "AG":
                    comboBoxNewLicensee.SelectedIndex = 146;
                    break;
                case "AH":
                    comboBoxNewLicensee.SelectedIndex = 147;
                    break;
                case "AK":
                    comboBoxNewLicensee.SelectedIndex = 148;
                    break;
                case "AL":
                    comboBoxNewLicensee.SelectedIndex = 149;
                    break;
                case "AP":
                    comboBoxNewLicensee.SelectedIndex = 150;
                    break;
                case "AQ":
                    comboBoxNewLicensee.SelectedIndex = 151;
                    break;
                case "B0":
                    comboBoxNewLicensee.SelectedIndex = 152;
                    break;
                case "B1":
                    comboBoxNewLicensee.SelectedIndex = 153;
                    break;
                case "B2":
                    comboBoxNewLicensee.SelectedIndex = 154;
                    break;
                case "B4":
                    comboBoxNewLicensee.SelectedIndex = 155;
                    break;
                case "B6":
                    comboBoxNewLicensee.SelectedIndex = 156;
                    break;
                case "B7":
                    comboBoxNewLicensee.SelectedIndex = 157;
                    break;
                case "B9":
                    comboBoxNewLicensee.SelectedIndex = 158;
                    break;
                case "BA":
                    comboBoxNewLicensee.SelectedIndex = 159;
                    break;
                case "BB":
                    comboBoxNewLicensee.SelectedIndex = 160;
                    break;
                case "BD":
                    comboBoxNewLicensee.SelectedIndex = 161;
                    break;
                case "BF":
                    comboBoxNewLicensee.SelectedIndex = 162;
                    break;
                case "BG":
                    comboBoxNewLicensee.SelectedIndex = 163;
                    break;
                case "BJ":
                    comboBoxNewLicensee.SelectedIndex = 164;
                    break;
                case "BL":
                    comboBoxNewLicensee.SelectedIndex = 165;
                    break;
                case "BN":
                    comboBoxNewLicensee.SelectedIndex = 166;
                    break;
                case "BP":
                    comboBoxNewLicensee.SelectedIndex = 167;
                    break;
                case "BQ":
                    comboBoxNewLicensee.SelectedIndex = 168;
                    break;
                case "C0":
                    comboBoxNewLicensee.SelectedIndex = 169;
                    break;
                case "C2":
                    comboBoxNewLicensee.SelectedIndex = 170;
                    break;
                case "C3":
                    comboBoxNewLicensee.SelectedIndex = 171;
                    break;
                case "C5":
                    comboBoxNewLicensee.SelectedIndex = 172;
                    break;
                case "C6":
                    comboBoxNewLicensee.SelectedIndex = 173;
                    break;
                case "C8":
                    comboBoxNewLicensee.SelectedIndex = 174;
                    break;
                case "CA":
                    comboBoxNewLicensee.SelectedIndex = 175;
                    break;
                case "CB":
                    comboBoxNewLicensee.SelectedIndex = 176;
                    break;
                case "CC":
                    comboBoxNewLicensee.SelectedIndex = 177;
                    break;
                case "CD":
                    comboBoxNewLicensee.SelectedIndex = 178;
                    break;
                case "CE":
                    comboBoxNewLicensee.SelectedIndex = 179;
                    break;
                case "CF":
                    comboBoxNewLicensee.SelectedIndex = 180;
                    break;
                case "CM":
                    comboBoxNewLicensee.SelectedIndex = 181;
                    break;
                case "CP":
                    comboBoxNewLicensee.SelectedIndex = 182;
                    break;
                case "D1":
                    comboBoxNewLicensee.SelectedIndex = 183;
                    break;
                case "D2":
                    comboBoxNewLicensee.SelectedIndex = 184;
                    break;
                case "D3":
                    comboBoxNewLicensee.SelectedIndex = 185;
                    break;
                case "D4":
                    comboBoxNewLicensee.SelectedIndex = 186;
                    break;
                case "D6":
                    comboBoxNewLicensee.SelectedIndex = 187;
                    break;
                case "D7":
                    comboBoxNewLicensee.SelectedIndex = 188;
                    break;
                case "D9":
                    comboBoxNewLicensee.SelectedIndex = 189;
                    break;
                case "DA":
                    comboBoxNewLicensee.SelectedIndex = 190;
                    break;
                case "DB":
                    comboBoxNewLicensee.SelectedIndex = 191;
                    break;
                case "DD":
                    comboBoxNewLicensee.SelectedIndex = 192;
                    break;
                case "DF":
                    comboBoxNewLicensee.SelectedIndex = 193;
                    break;
                case "DH":
                    comboBoxNewLicensee.SelectedIndex = 194;
                    break;
                case "DN":
                    comboBoxNewLicensee.SelectedIndex = 195;
                    break;
                case "E2":
                    comboBoxNewLicensee.SelectedIndex = 196;
                    break;
                case "E3":
                    comboBoxNewLicensee.SelectedIndex = 197;
                    break;
                case "E5":
                    comboBoxNewLicensee.SelectedIndex = 198;
                    break;
                case "E7":
                    comboBoxNewLicensee.SelectedIndex = 199;
                    break;
                case "E8":
                    comboBoxNewLicensee.SelectedIndex = 200;
                    break;
                case "E9":
                    comboBoxNewLicensee.SelectedIndex = 201;
                    break;
                case "EA":
                    comboBoxNewLicensee.SelectedIndex = 202;
                    break;
                case "EB":
                    comboBoxNewLicensee.SelectedIndex = 203;
                    break;
                case "EC":
                    comboBoxNewLicensee.SelectedIndex = 204;
                    break;
                case "EE":
                    comboBoxNewLicensee.SelectedIndex = 205;
                    break;
                case "EL":
                    comboBoxNewLicensee.SelectedIndex = 206;
                    break;
                case "EM":
                    comboBoxNewLicensee.SelectedIndex = 207;
                    break;
                case "EN":
                    comboBoxNewLicensee.SelectedIndex = 208;
                    break;
                case "F0":
                    comboBoxNewLicensee.SelectedIndex = 209;
                    break;
                case "G1":
                    comboBoxNewLicensee.SelectedIndex = 210;
                    break;
                case "G4":
                    comboBoxNewLicensee.SelectedIndex = 211;
                    break;
                case "G5":
                    comboBoxNewLicensee.SelectedIndex = 212;
                    break;
                case "G6":
                    comboBoxNewLicensee.SelectedIndex = 213;
                    break;
                case "G7":
                    comboBoxNewLicensee.SelectedIndex = 214;
                    break;
                case "G8":
                    comboBoxNewLicensee.SelectedIndex = 215;
                    break;
                case "G9":
                    comboBoxNewLicensee.SelectedIndex = 216;
                    break;
                case "GB":
                    comboBoxNewLicensee.SelectedIndex = 217;
                    break;
                case "GD":
                    comboBoxNewLicensee.SelectedIndex = 218;
                    break;
                case "HY":
                    comboBoxNewLicensee.SelectedIndex = 219;
                    break;
                default:
                    comboBoxNewLicensee.SelectedIndex = 220;
                    break;
            }
        }

        private void fixChecksumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameBoyROMInfo gameBoyROMInfo = new GameBoyROMInfo(filepath);
            string correctHeaderChecksum = gameBoyROMInfo.getCorrectHeaderChecksum();
            textBoxCorrectHeaderChecksum.Text = correctHeaderChecksum;
            textBoxHeaderChecksum.Text = correctHeaderChecksum;

            bool result = gameBoyROMInfo.setHeaderChecksum(correctHeaderChecksum);

            if(result)
            {
                MessageBox.Show("Checksum fixed!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("There was a problem fixing the checksum.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }

            validateHeaderChecksum();
        }

        private void writeROMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameBoyROMInfo gameBoyROMInfo = new GameBoyROMInfo(filepath);

            // Get comboBox data
            //string romSizeValue = ((KeyValuePair<string, string>)comboBoxROMSize.SelectedItem).Value;
            string cartridgeTypeKey = ((KeyValuePair<string, string>)comboBoxCartridgeType.SelectedItem).Key;
            string romSizeKey = ((KeyValuePair<string, string>)comboBoxROMSize.SelectedItem).Key;
            string ramSizeKey = ((KeyValuePair<string, string>)comboBoxRAMSize.SelectedItem).Key;
            string destinationCodeKey = ((KeyValuePair<string, string>)comboBoxDestinationCode.SelectedItem).Key;
            string oldLicenseeKey = ((KeyValuePair<string, string>)comboBoxOldLicensee.SelectedItem).Key;
            string newLicenseeKey = ((KeyValuePair<string, string>)comboBoxNewLicensee.SelectedItem).Key;

            string romEntryPoint = textBoxROMEntryPoint.Text.ToUpper();
            string gameTitle = textBoxGameTitle.Text.ToUpper();
            string manufacturerCode = textBoxManufacturerCode.Text.ToUpper();
            string versionNumber = textBoxVersionNumber.Text.ToUpper();
            string globalChecksum = textBoxGlobalChecksum.Text.ToUpper();
            string headerChecksum = textBoxHeaderChecksum.Text.ToUpper();
            string correctedHeaderChecksum = textBoxCorrectHeaderChecksum.Text.ToUpper();
            string nintendoLogo = textBoxNintendoLogo.Text.ToUpper();

            bool isGameBoy = checkBoxGameBoy.Checked;
            bool isColorGameBoy = checkBoxColorGameBoy.Checked;
            bool isGameBoyAndColorGameBoy = (isGameBoy && isColorGameBoy);
            bool isSuperGameBoy = checkBoxIsSuperGameBoy.Checked;

            bool result1 = true;
            bool result2 = true;
            bool result3 = true;
            bool result4 = true;
            bool result5 = true;
            bool result6 = true;

            if(cartridgeTypeKey != "??")
            {
                result1 = gameBoyROMInfo.setCartridgeTypeCode(cartridgeTypeKey);
            }

            if(romSizeKey != "??")
            {
                result2 = gameBoyROMInfo.setROMSizeCode(romSizeKey);
            }

            if(ramSizeKey != "??")
            {
                result3 = gameBoyROMInfo.setRAMSizeCode(ramSizeKey);
            }

            if(destinationCodeKey != "??")
            {
                result4 = gameBoyROMInfo.setDestinationCode(destinationCodeKey);
            }

            if(oldLicenseeKey != "??")
            {
                result5 = gameBoyROMInfo.setOldLicenseeCode(oldLicenseeKey);
            }

            if(newLicenseeKey != "??")
            {
                result6 = gameBoyROMInfo.setNewLicenseeCode(newLicenseeKey);
            }

            bool result7 = gameBoyROMInfo.setROMEntryPoint(romEntryPoint);
            bool result8 = gameBoyROMInfo.setGameTitle(gameTitle);
            bool result9 = gameBoyROMInfo.setManufacturerCode(manufacturerCode);
            bool result10 = gameBoyROMInfo.setVersionNumber(versionNumber);
            bool result11 = gameBoyROMInfo.setGlobalChecksum(globalChecksum);
            bool result12 = gameBoyROMInfo.setHeaderChecksum(headerChecksum);
            //gameBoyROMInfo.setHeaderChecksum(correctedHeaderChecksum);
            bool result13 = gameBoyROMInfo.setNintendoLogo(nintendoLogo);
            bool result14 = gameBoyROMInfo.setSuperGameBoySupport(isSuperGameBoy);

            
            bool result15 = true;
            if(hasColorGameBoyFeaturesEnabled)
            {
                result15 = gameBoyROMInfo.setGameBoyAndColorGameBoySupport(isGameBoyAndColorGameBoy);
            }

            #region errorMessage
            if(!result1 || !result2 || !result3 || !result4 || !result5 || !result6 || !result7 || !result8 || !result9 || !result10 || !result11 || !result12 
                || !result13 || !result14 || !result15)
            {
                string errorMsg = "Unable to update: ";

                if(!result1)
                {
                    errorMsg += "Catridge Type, ";
                }

                if(!result2)
                {
                    errorMsg += "ROM Size, ";
                }

                if(!result3)
                {
                    errorMsg += "RAM Size, ";
                }

                if(!result4)
                {
                    errorMsg += "Destination Code, ";
                }

                if(!result5)
                {
                    errorMsg += "Old Licensee Code, ";
                }

                if(!result6)
                {
                    errorMsg += "New Licensee Code, ";
                }

                if(!result7)
                {
                    errorMsg += "ROM Entry Point, ";
                }

                if(!result8)
                {
                    errorMsg += "Game Title, ";
                }

                if(!result9)
                {
                    errorMsg += "Manufacturer Code, ";
                }

                if(!result10)
                {
                    errorMsg += "Version Number, ";
                }

                if(!result11)
                {
                    errorMsg += "Global Checksum, ";
                }

                if(!result12)
                {
                    errorMsg += "Header Checksum, ";
                }

                if(!result13)
                {
                    errorMsg += "Nintendo Logo, ";
                }

                if(!result14)
                {
                    errorMsg += "Super Game Boy Support, ";
                }

                if(!result15)
                {
                    errorMsg += "Game Boy And Color Game Boy Support, ";
                }

                errorMsg = errorMsg.TrimEnd(' ');
                errorMsg = errorMsg.TrimEnd(',');

                MessageBox.Show("There was a problem updating the ROM header. " + errorMsg + ".",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("ROM header updated!.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
            }
            #endregion

            parseROMFile(filepath);

        }

        private void checkBoxIsSuperGameBoy_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxIsSuperGameBoy.Checked)
            {
                // A value of 33h signalizes that the New License Code in header bytes 0144-0145 is used instead. 
                // Super GameBoy functions won't work if <> 0x33.
                comboBoxOldLicensee.SelectedIndex = 20;
            }
        }

        private void restoreNintendoLogoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GameBoyROMInfo gameBoyROMInfo = new GameBoyROMInfo(filepath);
            string nintendoLogo = "CEED6666CC0D000B03730083000C000D0008111F8889000EDCCC6EE6DDDDD999BBBB67636E0EECCCDDDC999FBBB933";
            textBoxNintendoLogo.Text = nintendoLogo;
            bool result = gameBoyROMInfo.setNintendoLogo(nintendoLogo);

            if(result)
            {
                MessageBox.Show("Nintendo logo restored!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("There was a problem restoring the Nintendo logo.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }

            validateNintendoLogo();
        }

        private void enableColorGameBoyOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(enableColorGameBoyOptionsToolStripMenuItem.Checked)
            {
                enableColorGameBoyOptionsToolStripMenuItem.Checked = false;
                hasColorGameBoyFeaturesEnabled = false;
                checkBoxColorGameBoy.Enabled = false;
                checkBoxGameBoy.Enabled = false;
            }
            else
            {
                enableColorGameBoyOptionsToolStripMenuItem.Checked = true;
                hasColorGameBoyFeaturesEnabled = true;
                checkBoxColorGameBoy.Enabled = true;
                checkBoxGameBoy.Enabled = true;
            }

            parseROMFile(filepath);
        }

        public void validateHexInput(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar >= 'A' && e.KeyChar <= 'F' || e.KeyChar >= 'a' && e.KeyChar <= 'f' || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void validateHeaderChecksum()
        {
            if(textBoxHeaderChecksum.Text != textBoxCorrectHeaderChecksum.Text)
            {
                textBoxHeaderChecksum.ForeColor = Color.Red;
            }
            else
            {
                textBoxHeaderChecksum.ForeColor = Color.Black;
            }
        }

        private void validateNintendoLogo()
        {
            string nintendoLogo = "CEED6666CC0D000B03730083000C000D0008111F8889000EDCCC6EE6DDDDD999BBBB67636E0EECCCDDDC999FBBB933";
            if(textBoxNintendoLogo.Text != nintendoLogo)
            {
                textBoxNintendoLogo.ForeColor = Color.Red;
            }
            else
            {
                textBoxNintendoLogo.ForeColor = Color.Black;
            }
        }
    }
}
