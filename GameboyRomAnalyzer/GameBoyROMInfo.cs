using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

// Shawn M. Crawford - [sleepy9090] - 2017
namespace GameboyRomAnalyzer
{
    class GameBoyROMInfo
    {
        string path;

        public GameBoyROMInfo(string gamePath)
        {
            path = gamePath;
        }

        public string getROMEntryPoint()
        {
            return getHexStringFromFile(0x100, 0x4);
        }

        public bool setROMEntryPoint(string romEntryPoint)
        {
            return writeByteArrayToFile(this.path, 0x100, StringToByteArray(romEntryPoint.PadRight(0x8, '0')));
        }

        public string getNintendoLogo()
        {
            return getHexStringFromFile(0x104, 0x2F);
        }

        public bool setNintendoLogo(string nintendoLogo)
        {
            return writeByteArrayToFile(this.path, 0x104, StringToByteArray(nintendoLogo.PadRight(0x5E, '0')));
        }

        public string getGameTitle()
        {
            return convertHexToAscii(getHexStringFromFile(0x134, 0x10));
        }

        public bool setGameTitle(string gameTitle)
        {
            string hexValue = convertAsciiToHex(gameTitle.PadRight(0x10));
            return writeByteArrayToFile(this.path, 0x134, StringToByteArray(hexValue));
        }

        public string getManufacturerCode()
        {
            return convertHexToAscii(getHexStringFromFile(0x13F, 0x4));
        }

        public bool setManufacturerCode(string manufacturerCode)
        {
            string hexValue = convertAsciiToHex(manufacturerCode.PadRight(0x4, '0'));
            return writeByteArrayToFile(this.path, 0x13F, StringToByteArray(hexValue));
        }

        // this is only for Color GameBoy
        // 80h - Game supports CGB functions, but works on old gameboys also.
        // C0h - Game works on CGB only (physically the same as 80h).
        public bool hasColorGameBoySupport()
        {
            bool result = false;
            string sgbHexString = getHexStringFromFile(0x143, 0x1);

            if(sgbHexString == "80" || sgbHexString == "C0")
            {
                result = true;
            }

            return result;
        }

        //// this is only for Color GameBoy
        //public bool setColorGameBoySupportOnly(bool hasColorGameBoySupportOnly)
        //{

        //    return writeByteArrayToFile(this.path, 0x143, StringToByteArray("C0"));
        //}

        // this is only for Color GameBoy
        // 80h - Game supports CGB functions, but works on old gameboys also.
        // C0h - Game works on CGB only (physically the same as 80h).
        public bool hasGameBoySupport()
        {
            bool result = false;
            string sgbHexString = getHexStringFromFile(0x143, 0x1);

            if(sgbHexString == "80")
            {
                result = true;
            }

            return result;
        }

        // this is only for Color GameBoy
        public bool setGameBoyAndColorGameBoySupport(bool hasGameBoyAndColorGameBoySupport)
        {
            bool result = false;

            if(hasGameBoyAndColorGameBoySupport)
            {
                result = writeByteArrayToFile(this.path, 0x143, StringToByteArray("80"));
            }
            else
            {
                result = writeByteArrayToFile(this.path, 0x143, StringToByteArray("C0"));
            }

            return result;
        }

        // 00h = No SGB functions (Normal Gameboy or CGB only game)
        // 03h = Game supports SGB functions
        public bool hasSuperGameBoySupport()
        {
            bool result = false;
            string sgbHexString = getHexStringFromFile(0x146, 0x1);

            if(sgbHexString == "03")
            {
                result = true;
            }

            return result;
        }

        public bool setSuperGameBoySupport(bool hasSuperGameBoySupport)
        {
            bool result = false;

            if(hasSuperGameBoySupport)
            {
                result = writeByteArrayToFile(this.path, 0x146, StringToByteArray("03"));
            }
            else
            {
                result = writeByteArrayToFile(this.path, 0x146, StringToByteArray("00"));
            }

            return result;
        }

        public string getDestinationCode()
        {
            return getHexStringFromFile(0x14A, 0x1);
        }

        public bool setDestinationCode(string destinationCode)
        {
            return writeByteArrayToFile(this.path, 0x14A, StringToByteArray(destinationCode.PadRight(0x1)));
        }

        public string getHeaderChecksum()
        {
            return getHexStringFromFile(0x14D, 0x1);
        }

        public bool setHeaderChecksum(string headerChecksum)
        {
            return writeByteArrayToFile(this.path, 0x14D, StringToByteArray(headerChecksum.PadRight(0x2, '0')));
        }

        public string getGlobalChecksum()
        {
            return getHexStringFromFile(0x14E, 0x2);
        }

        public bool setGlobalChecksum(string globalChecksum)
        {
            return writeByteArrayToFile(this.path, 0x14E, StringToByteArray(globalChecksum.PadRight(0x4, '0')));
        }

        public string getVersionNumber()
        {
            return getHexStringFromFile(0x14C, 0x1);
        }

        public bool setVersionNumber(string versionNumber)
        {
            return writeByteArrayToFile(this.path, 0x14C, StringToByteArray(versionNumber.PadRight(0x2, '0')));
        }

        public string getRAMSizeCode()
        {
            return getHexStringFromFile(0x149, 0x1);
        }

        public bool setRAMSizeCode(string ramSizeCode)
        {
            return writeByteArrayToFile(this.path, 0x149, StringToByteArray(ramSizeCode.PadRight(0x1)));
        }

        public string getROMSizeCode()
        {
            return getHexStringFromFile(0x148, 0x1);
        }

        public bool setROMSizeCode(string romSizeCode)
        {
            return writeByteArrayToFile(this.path, 0x148, StringToByteArray(romSizeCode.PadRight(0x1)));
        }

        public string getCartridgeTypeCode()
        {
            return getHexStringFromFile(0x147, 0x1);
        }

        public bool setCartridgeTypeCode(string cartridgeTypeCode)
        {
            return writeByteArrayToFile(this.path, 0x147, StringToByteArray(cartridgeTypeCode.PadRight(0x1)));
        }

        public string getOldLicenseeCode()
        {
            return getHexStringFromFile(0x14B, 0x1);
        }

        public bool setOldLicenseeCode(string oldLicenseeCode)
        {
            return writeByteArrayToFile(this.path, 0x14B, StringToByteArray(oldLicenseeCode.PadRight(0x1)));
        }

        public string getNewLicenseeCode()
        {
            return convertHexToAscii(getHexStringFromFile(0x144, 0x2));
        }

        public bool setNewLicenseeCode(string newLicenseeCode)
        {
            string hexValue = convertAsciiToHex(newLicenseeCode.PadRight(0x2));
            return writeByteArrayToFile(this.path, 0x144, StringToByteArray(hexValue));
        }

        // x=0:FOR i=0134h TO 014Ch:x=x-MEM[i]-1:NEXT
        public string getCorrectHeaderChecksum()
        {
            int checksum = 0;
            string checksumString = "";
            // To calculate the header checksum, start at the address 0x0134. 
            // For each byte until address 0x014C, the calculated checksum (starting at 0) equals the calculated checksum - the byte you are currently at - 1.
            // The checksum is the last byte of the result

            string checksumData = getHexStringFromFile(0x134, 0x19);

            int data1 = Convert.ToInt32(checksumData.Substring(0, 2), 16);
            int data2 = Convert.ToInt32(checksumData.Substring(2, 2), 16);
            int data3 = Convert.ToInt32(checksumData.Substring(4, 2), 16);
            int data4 = Convert.ToInt32(checksumData.Substring(6, 2), 16);
            int data5 = Convert.ToInt32(checksumData.Substring(8, 2), 16);
            int data6 = Convert.ToInt32(checksumData.Substring(10, 2), 16);
            int data7 = Convert.ToInt32(checksumData.Substring(12, 2), 16);
            int data8 = Convert.ToInt32(checksumData.Substring(14, 2), 16);
            int data9 = Convert.ToInt32(checksumData.Substring(16, 2), 16);
            int data10 = Convert.ToInt32(checksumData.Substring(18, 2), 16);
            int data11 = Convert.ToInt32(checksumData.Substring(20, 2), 16);
            int data12 = Convert.ToInt32(checksumData.Substring(22, 2), 16);
            int data13 = Convert.ToInt32(checksumData.Substring(24, 2), 16);
            int data14 = Convert.ToInt32(checksumData.Substring(26, 2), 16);
            int data15 = Convert.ToInt32(checksumData.Substring(28, 2), 16);
            int data16 = Convert.ToInt32(checksumData.Substring(30, 2), 16);
            int data17 = Convert.ToInt32(checksumData.Substring(32, 2), 16);
            int data18 = Convert.ToInt32(checksumData.Substring(34, 2), 16);
            int data19 = Convert.ToInt32(checksumData.Substring(36, 2), 16);
            int data20 = Convert.ToInt32(checksumData.Substring(38, 2), 16);
            int data21 = Convert.ToInt32(checksumData.Substring(40, 2), 16);
            int data22 = Convert.ToInt32(checksumData.Substring(42, 2), 16);
            int data23 = Convert.ToInt32(checksumData.Substring(44, 2), 16);
            int data24 = Convert.ToInt32(checksumData.Substring(46, 2), 16);
            int data25 = Convert.ToInt32(checksumData.Substring(48, 2), 16);

            checksum = 0 - data1 - data2 - data3 - data4 - data5 - data6 - data7 - data8 - data9 - data10 - data11 - data12 - data13 
                - data14 - data15 - data16 - data17 - data18 - data19 - data20 - data21 - data22 - data23 - data24 - data25
                -0x19;

            //"544554524953000000000000000000000000000000000001010A"
            //"5341474132000000000000000000000000000003030201C300CD"
            checksumString = checksum.ToString("X2");
            checksumString = checksumString.Substring(Math.Max(0, checksumString.Length - 2));
            return checksumString;
        }

        private static string convertAsciiToHex(String asciiString)
        {
            char[] charValues = asciiString.ToCharArray();
            string hexValue = "";
            foreach(char c in charValues)
            {
                int value = Convert.ToInt32(c);
                hexValue += String.Format("{0:X}", value);
            }
            return hexValue;
        }

        private static string convertHexToAscii(String hexString)
        {
            try
            {
                string ascii = string.Empty;

                for(int i = 0; i < hexString.Length; i += 2)
                {
                    String hs = string.Empty;

                    hs = hexString.Substring(i, 2);
                    uint decval = System.Convert.ToUInt32(hs, 16);
                    char character = System.Convert.ToChar(decval);
                    ascii += character;

                }

                return ascii;
            }
            catch(Exception ex)
            {
                // Console.WriteLine(ex.Message);
            }

            return string.Empty;
        }

        private string getHexStringFromFile(int startPoint, int length)
        {
            string hexString = "";
            using(FileStream fileStream = new FileStream(@path, FileMode.Open, FileAccess.Read))
            {
                long offset = fileStream.Seek(startPoint, SeekOrigin.Begin);

                for(int x = 0; x < length; x++)
                {
                    hexString += fileStream.ReadByte().ToString("X2");
                }

            }

            return hexString;
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        private bool writeByteArrayToFile(string fileName, int startPoint, byte[] byteArray)
        {
            bool result = false;
            try
            {
                using(var fs = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                {
                    fs.Position = startPoint;
                    fs.Write(byteArray, 0, byteArray.Length);
                    result = true;
                }
            }
            catch(Exception ex)
            {
                // Console.WriteLine("Error writing file: {0}", ex);
                result = false;
            }

            return result;
        }
    }
}
