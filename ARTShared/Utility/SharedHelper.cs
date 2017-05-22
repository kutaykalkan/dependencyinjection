using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkyStem.ART.Shared.Data;
using SkyStem.ART.Client.Model;
using System.Runtime.Serialization.Formatters.Binary;
using SkyStem.Language.LanguageUtility.Classes;
using SkyStem.ART.Client.Data;
using System.Globalization;
using SkyStem.Language.LanguageUtility;
using System.IO;
using SkyStem.ART.Client.Exception;
using SkyStem.ART.Web.Utility;

namespace SkyStem.ART.Shared.Utility
{
    public class SharedHelper
    {
        private SharedHelper()
        {
        }

        private const string ALLOWED_PASSWORD_CHAR_SMALL_CAPS = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        private const string ALLOWED_PASSWORD_CHAR_CAPS = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        private const string ALLOWED_PASSWORD_CHAR_NUMBERS = "1,2,3,4,5,6,7,8,9,0";
        private const string ALLOWED_PASSWORD_CHAR_SPECIAL_CHARS = "!,@,#,$,%,&,?";

        public static string ReplaceSpecialChars(string str)
        {
            str = str.Replace("'", "''");
            return str;
        }

        /// <summary>
        /// Clone the Object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object DeepClone(object obj)
        {
            if (obj == null)
                return null;
            object oNewObject;
            //  Create a memory stream and a formatter.
            System.IO.MemoryStream oMemoryStream = new System.IO.MemoryStream(1000);
            BinaryFormatter oBinaryFormatter = new BinaryFormatter();
            //  Serialize the object into the stream.
            oBinaryFormatter.Serialize(oMemoryStream, obj);
            //  Position streem pointer back to first byte.
            oMemoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            //  Deserialize into another object.
            oNewObject = oBinaryFormatter.Deserialize(oMemoryStream);
            //  Release memory.
            oMemoryStream.Close();

            return oNewObject;
        }

        public static int? GetMonthsBetweenDates(DateTime? dtTo, DateTime? dtFrom)
        {
            if (dtFrom.HasValue && dtTo.HasValue && dtTo.Value >= dtFrom.Value)
            {
                int BeginYear = dtFrom.Value.Year;
                int BeginMonth = dtFrom.Value.Month;
                int EndYear = dtTo.Value.Year;
                int EndMonth = dtTo.Value.Month;
                int totalMonths = (EndYear - BeginYear) * 12 + EndMonth - BeginMonth + 1;
                return totalMonths;
            }
            return null;
        }

        #region Display Methods
        public static string GetDisplayDate(DateTime? oDate, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            if (oDate == null || oDate == default(DateTime))
            {
                return SharedConstants.HYPHEN;
            }
            else
            {
                string dtFormat = null;
                if (oMultilingualAttributeInfo != null)
                    dtFormat = LanguageUtil.GetValue(2, oMultilingualAttributeInfo);
                else
                    LanguageUtil.GetValue(2);
                return oDate.Value.ToString(dtFormat);
            }
        }

        public static string GetDisplayDateTime(DateTime? oDate, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            if (oDate == null || oDate == default(DateTime))
            {
                return SharedConstants.HYPHEN;
            }
            else
            {
                string dtFormat = LanguageUtil.GetValue(2838, oMultilingualAttributeInfo);
                return oDate.Value.ToString(dtFormat);
            }
        }

        public static string GetDisplayDecimalValue(Decimal? val, int deciPlaces)
        {

            if (val == null || val == DecimalConstants.NULL_DECIMAL)
            {
                return SharedConstants.HYPHEN;
            }
            else
            {
                NumberFormatInfo oNumberFormatInfo = new NumberFormatInfo();
                oNumberFormatInfo.NumberDecimalDigits = deciPlaces;
                return val.Value.ToString("N", oNumberFormatInfo);
            }
        }

        public static string GetDisplayValueByImportFieldID(string data, string importFieldID, MultilingualAttributeInfo oMultilingualAttributeInfo)
        {
            string value = data;
            int fieldID = 0;
            if (string.IsNullOrEmpty(importFieldID) || string.IsNullOrEmpty(data))
                return data;
            fieldID = Convert.ToInt32(importFieldID);
            switch ((ARTEnums.DataImportFields)fieldID)
            {
                case ARTEnums.DataImportFields.BalanceInBaseCCY:
                case ARTEnums.DataImportFields.BalanceInReportingCCY:
                    decimal DecimalValue = 0;
                    if (Decimal.TryParse(data, out DecimalValue))
                        value = SharedHelper.GetDisplayDecimalValue(DecimalValue, DecimalConstants.DECIMAL_PLACES_FOR_MATH_ROUND);
                    break;
                case ARTEnums.DataImportFields.PeriodEndDate:
                    DateTime DateValue = DateTime.MinValue;
                    if (DateTime.TryParse(data, out DateValue))
                        value = SharedHelper.GetDisplayDate(DateValue, oMultilingualAttributeInfo);
                    break;
                default:
                    value = data;
                    break;
            }
            return value;
        }

        public static string GetDisplayFilePath(string filePath)
        {
            return filePath;
            //return filePath.Replace(SharedDataImportHelper.GetBaseFolder(), "");
        }
        #endregion

        #region Password Methods

        public static bool IsPassowrdValidByPolicy(string password, int minPasswordLength, string loginID)
        {
            if (IsPassowrdValidByLengthRule(password, minPasswordLength)
                && IsPassowrdValidByLoginIDRule(password, loginID)
                && IsPasswordValidByThreeCharTypeRule(password))
                return true;
            return false;
        }

        public static bool IsPassowrdValidByLengthRule(string password, int minPasswordLength)
        {
            if (password.Length >= minPasswordLength)
                return true;
            return false;
        }

        public static bool IsPassowrdValidByLoginIDRule(string password, string loginID)
        {
            bool isValid = true;
            if (loginID.Length > 2)
            {
                for (int i = 0; i < loginID.Length - 2; i++)
                {
                    string searchStr = loginID.Substring(i, 3);
                    if (password.IndexOf(searchStr, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            return isValid;
        }

        public static bool IsPasswordValidByThreeCharTypeRule(string password)
        {
            char[] sep = { ',' };
            int ruleCount = 0;
            string[] arrallowedChars1 = ALLOWED_PASSWORD_CHAR_SMALL_CAPS.Split(sep);
            string[] arrallowedChars2 = ALLOWED_PASSWORD_CHAR_CAPS.Split(sep);
            string[] arrallowedChars3 = ALLOWED_PASSWORD_CHAR_NUMBERS.Split(sep);
            string[] arrallowedChars4 = ALLOWED_PASSWORD_CHAR_SPECIAL_CHARS.Split(sep);
            foreach(string val in arrallowedChars1)
            {
                if (password.IndexOf(val) >= 0)
                {
                    ruleCount++;
                    break;
                }
            }
            foreach (string val in arrallowedChars2)
            {
                if (password.IndexOf(val) >= 0)
                {
                    ruleCount++;
                    break;
                }
            }
            foreach (string val in arrallowedChars3)
            {
                if (password.IndexOf(val) >= 0)
                {
                    ruleCount++;
                    break;
                }
            }
            foreach (string val in arrallowedChars4)
            {
                if (password.IndexOf(val) >= 0)
                {
                    ruleCount++;
                    break;
                }
            }
            if (ruleCount >= 3)
                return true;
            return false;
        }

        public static string CreateRandomPassword(int passwordLength, string loginID)
        {
            Random rand = new Random();
            string password = CreateRandomPassword(passwordLength, loginID, rand);
            return password;
        }

        public static string CreateRandomPassword(int passwordLength, string loginID, Random rand)
        {
            int attemptCount = 5;
            bool isValid = false;
            string password = CreateRandomPassword(passwordLength, rand);
            while (attemptCount > 0)
            {
                isValid = IsPassowrdValidByLoginIDRule(password, loginID);
                if (isValid)
                    return password;
                else
                    password = CreateRandomPassword(passwordLength, rand);
                attemptCount--;
            }
            if (attemptCount == 0)
                throw new ARTException(1721);
            return password;
        }


        /// <summary>
        /// At least one Caps and one Number condition is fullfilled.
        /// Altrenate is to check in the last if atleast one CAP and number exist(may be use regExp) if not then add them randomly 
        /// </summary>
        /// <param name="passwordLength"></param>
        /// <returns></returns>
        private static string CreateRandomPassword(int passwordLength, Random rand)
        {
            string passwordString = "";
            string allowedChars = "";
            char[] sep = { ',' };
            string[] arrallowedChars1 = ALLOWED_PASSWORD_CHAR_SMALL_CAPS.Split(sep);
            string[] arrallowedChars2 = ALLOWED_PASSWORD_CHAR_CAPS.Split(sep);
            string[] arrallowedChars3 = ALLOWED_PASSWORD_CHAR_NUMBERS.Split(sep);
            string[] arrallowedChars4 = ALLOWED_PASSWORD_CHAR_SPECIAL_CHARS.Split(sep);
            allowedChars = ALLOWED_PASSWORD_CHAR_SMALL_CAPS + "," + ALLOWED_PASSWORD_CHAR_CAPS + "," + ALLOWED_PASSWORD_CHAR_NUMBERS + "," + ALLOWED_PASSWORD_CHAR_SPECIAL_CHARS;

            string atLeastOneSmallCap = arrallowedChars1[rand.Next(0, arrallowedChars1.Length)];
            string atLeastOneCap = arrallowedChars2[rand.Next(0, arrallowedChars2.Length)];
            string atLeastOneNumber = arrallowedChars3[rand.Next(0, arrallowedChars3.Length)];
            string atLeastOneSpecial = arrallowedChars4[rand.Next(0, arrallowedChars4.Length)];
            int placeOfSmallCap = -1;
            int placeOfCap = -1;
            int placeOfNumber = -1;
            int placeOfSpecial = -1;
            bool? useFirstHalf = null;
            while (placeOfSmallCap == -1 || placeOfCap == -1 || placeOfNumber == -1 || placeOfSpecial == -1)
            {
                int index = -1;
                if (!useFirstHalf.HasValue)
                    index = rand.Next(1, passwordLength + 1);
                else if (useFirstHalf.Value)
                    index = rand.Next(1, passwordLength / 2);
                else
                    index = rand.Next(passwordLength / 2 + 1, passwordLength + 1);

                if (placeOfSmallCap == -1)
                    placeOfSmallCap = index;
                if (placeOfCap == -1 && index != placeOfSmallCap)
                    placeOfCap = index;
                if (placeOfNumber == -1 && index != placeOfSmallCap && index != placeOfCap)
                    placeOfNumber = index;
                if (placeOfSpecial == -1 && index != placeOfSmallCap && index != placeOfCap && index != placeOfNumber)
                    placeOfSpecial = index;
                if (index <= passwordLength / 2)
                    useFirstHalf = false;
                else
                    useFirstHalf = true;
            }
            string[] arr = allowedChars.Split(sep);
            string temp = "";
            for (int i = 0; i < passwordLength; i++)
            {
                if (i == placeOfSmallCap - 1)
                {
                    temp = atLeastOneSmallCap;
                }
                if (i == placeOfCap - 1)
                {
                    temp = atLeastOneCap;
                }
                else if (i == placeOfNumber - 1)
                {
                    temp = atLeastOneNumber;
                }
                else if (i == placeOfSpecial - 1)
                {
                    temp = atLeastOneSpecial;
                }
                else
                {
                    temp = arr[rand.Next(0, arr.Length)];
                }
                passwordString += temp;
            }
            // Apoorv - in case Password contains a &# 
            // it is taken as HTML tag, so remove # and replace with @
            if (passwordString.IndexOf("&#") != -1)
            {
                passwordString = passwordString.Replace("&#", "&@");
            }
            return passwordString;
        }
        #endregion
    }
}
