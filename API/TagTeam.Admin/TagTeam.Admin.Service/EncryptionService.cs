﻿using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TagTeam.Admin.Domain;
using TagTeam.Admin.Domain.CustomModels;
using TagTeam.Admin.Service.Interfaces;

namespace TagTeam.Admin.Service
{
    public class EncryptionService
    {
        public string ReturnEncryptedPassword(string password, string username)
        {
            char[] arrName = username.ToCharArray();
            char[] arrPwd = password.ToCharArray();

            char[] arrNamePwd = new char[password.Length + username.Length];

            //when userName length equal to the password length 
            if (username.Length == password.Length)
            {
                var x = 0;//for incement array of name
                for (int i = 0; i < (username.Length + password.Length); i++)
                {
                    arrNamePwd[i] = arrName[x];
                    i = i + 1;
                    x = x + 1;
                }

                var y = 0;
                for (int i = 1; i < (username.Length + password.Length); i++)
                {
                    arrNamePwd[i] = arrPwd[y];
                    i = i + 1;
                    y = y + 1;
                }
            }

            //when the password length max than username length.
            else if (username.Length < password.Length)
            {
                var x = 0;
                for (int i = 0; i < (username.Length + username.Length - 1); i++)
                {
                    arrNamePwd[i] = arrName[x];
                    i = i + 1;
                    x = x + 1;
                }

                var y = 0;
                for (int i = 1; i < (username.Length + username.Length); i++)
                {
                    arrNamePwd[i] = arrPwd[y];
                    i = i + 1;
                    y = y + 1;
                }

                for (int i = (username.Length + username.Length); i < (username.Length + password.Length); i++)
                {
                    arrNamePwd[i] = arrPwd[y];
                    y = y + 1;
                }

            }

            else
            {
                //username length max than password length
                var x = 0;
                for (int i = 0; i < (password.Length + password.Length - 1); i++)
                {
                    arrNamePwd[i] = arrPwd[x];
                    i = i + 1;
                    x = x + 1;
                }

                var y = 0;
                for (int i = 1; i < (password.Length + password.Length); i++)
                {
                    arrNamePwd[i] = arrName[y];
                    i = i + 1;
                    y = y + 1;
                }

                for (int i = (password.Length + password.Length); i < (password.Length + username.Length); i++)
                {
                    arrNamePwd[i] = arrName[y];
                    y = y + 1;
                }
            }

            //build up HexaAndDecimal array with using NamePwd array.

            string[] ConvertedHexAndDecimal = new string[arrNamePwd.Length * 5];

            int count = 0;

            int constIntValue = 0;

            for (int i = 0; i < arrNamePwd.Length; i++)
            {
                int value = Convert.ToInt32(arrNamePwd[i]);
                constIntValue = constIntValue + value % 10;
            }

            if (constIntValue % 10 == 0)
            {
                constIntValue = 1;
            }

            for (int i = 0; i < arrNamePwd.Length; i++)
            {
                int value = Convert.ToInt32(arrNamePwd[i]);

                decimal decValue = arrNamePwd[i] * (constIntValue % 10);

                string hexaOutput = String.Format("{0:X}", value * (constIntValue % 10));

                string decimalOutput = decValue.ToString().PadLeft(3, '0');

                ConvertedHexAndDecimal[count] = decimalOutput.ToCharArray()[0].ToString();
                ConvertedHexAndDecimal[count + 1] = hexaOutput.ToCharArray()[0].ToString();
                ConvertedHexAndDecimal[count + 2] = decimalOutput.ToCharArray()[1].ToString();
                ConvertedHexAndDecimal[count + 3] = hexaOutput.ToCharArray()[1].ToString();
                ConvertedHexAndDecimal[count + 4] = decimalOutput.ToCharArray()[2].ToString();

                count = count + 5;
            }

            //convert string array into char array
            char[] charHexDecimalArr = string.Join(string.Empty, ConvertedHexAndDecimal).ToCharArray();

            //declare final array
            char[] finalArr = new char[charHexDecimalArr.Length];

            for (int i = 0; i < charHexDecimalArr.Length; i++)
            {
                char val = charHexDecimalArr[i];

                switch (val)
                {
                    case '1':
                        {
                            finalArr[i] = 'a';
                            break;
                        }
                    case '2':
                        {
                            finalArr[i] = 'x';
                            break;
                        }
                    case '3':
                        {
                            finalArr[i] = 'B';
                            break;
                        }
                    case '4':
                        {
                            finalArr[i] = '*';
                            break;
                        }
                    case '5':
                        {
                            finalArr[i] = 'c';
                            break;
                        }
                    case '6':
                        {
                            finalArr[i] = 'M';
                            break;
                        }
                    case '7':
                        {
                            finalArr[i] = 'T';
                            break;
                        }
                    case '8':
                        {
                            finalArr[i] = '2';
                            break;
                        }
                    case '9':
                        {
                            finalArr[i] = 'N';
                            break;
                        }
                    case '0':
                        {
                            finalArr[i] = '6';
                            break;
                        }
                    case 'A':
                        {
                            finalArr[i] = 'z';
                            break;
                        }
                    case 'B':
                        {
                            finalArr[i] = 'y';
                            break;
                        }
                    case 'C':
                        {
                            finalArr[i] = 'o';
                            break;
                        }
                    case 'D':
                        {
                            finalArr[i] = 'p';
                            break;
                        }
                    case 'E':
                        {
                            finalArr[i] = 'v';
                            break;
                        }

                    default:
                        {
                            finalArr[i] = 'X';
                            break;
                        }
                }

                //insert into final array
                for (int l = i; l < charHexDecimalArr.Length; l++)
                {
                    finalArr[l] = finalArr[i];
                }

            }
            string FinalResult = new string(finalArr);
            return FinalResult;
        }

        public string ReturnEncryptedUserName(string username)
        {
            //return
            char[] arrName = username.ToCharArray();
            //char[] arrPwd = password.ToCharArray();

            char[] arrNamePwd = new char[username.Length];

            //build up HexaAndDecimal array with using NamePwd array.

            string[] ConvertedHexAndDecimal = new string[arrNamePwd.Length * 5];

            int count = 0;

            int constIntValue = 0;

            for (int i = 0; i < arrName.Length; i++)
            {
                int value = Convert.ToInt32(arrName[i]);
                constIntValue = constIntValue + value % 10;
            }

            if (constIntValue % 10 == 0)
            {
                constIntValue = 1;
            }

            for (int i = 0; i < arrName.Length; i++)
            {
                int value = Convert.ToInt32(arrName[i]);

                decimal decValue = arrName[i] * (constIntValue % 10);

                string hexaOutput = String.Format("{0:X}", value * (constIntValue % 10));

                string decimalOutput = decValue.ToString().PadLeft(3, '0');

                ConvertedHexAndDecimal[count] = decimalOutput.ToCharArray()[0].ToString();
                ConvertedHexAndDecimal[count + 1] = hexaOutput.ToCharArray()[0].ToString();
                ConvertedHexAndDecimal[count + 2] = decimalOutput.ToCharArray()[1].ToString();
                ConvertedHexAndDecimal[count + 3] = hexaOutput.ToCharArray()[1].ToString();
                ConvertedHexAndDecimal[count + 4] = decimalOutput.ToCharArray()[2].ToString();


                count = count + 5;
            }

            //convert string array into char array
            char[] charHexDecimalArr = string.Join(string.Empty, ConvertedHexAndDecimal).ToCharArray();

            //declare final array
            char[] finalArr = new char[charHexDecimalArr.Length];

            for (int i = 0; i < charHexDecimalArr.Length; i++)
            {
                char val = charHexDecimalArr[i];

                switch (val)
                {
                    case '1':
                        {
                            finalArr[i] = 'a';
                            break;
                        }
                    case '2':
                        {
                            finalArr[i] = 'x';
                            break;
                        }
                    case '3':
                        {
                            finalArr[i] = 'B';
                            break;
                        }
                    case '4':
                        {
                            finalArr[i] = 'D';
                            break;
                        }
                    case '5':
                        {
                            finalArr[i] = 'c';
                            break;
                        }
                    case '6':
                        {
                            finalArr[i] = 'm';
                            break;
                        }
                    case '7':
                        {
                            finalArr[i] = 'X';
                            break;
                        }
                    case '8':
                        {
                            finalArr[i] = '2';
                            break;
                        }
                    case '9':
                        {
                            finalArr[i] = 'R';
                            break;
                        }
                    case '0':
                        {
                            finalArr[i] = 'n';
                            break;
                        }
                    case 'A':
                        {
                            finalArr[i] = 'z';
                            break;
                        }
                    case 'B':
                        {
                            finalArr[i] = 'Y';
                            break;
                        }
                    case 'C':
                        {
                            finalArr[i] = 'o';
                            break;
                        }
                    case 'D':
                        {
                            finalArr[i] = 'C';
                            break;
                        }
                    case 'E':
                        {
                            finalArr[i] = 'v';
                            break;
                        }

                    default:
                        {
                            finalArr[i] = 'h';
                            break;
                        }
                }

                //insert into final array
                for (int l = i; l < charHexDecimalArr.Length; l++)
                {
                    finalArr[l] = finalArr[i];
                }

            }
            string FinalResult = new string(finalArr);
            return FinalResult;
        }
    }
}
