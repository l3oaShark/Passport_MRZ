using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passport_MRZ.Models
{
    public class PassportModel : DocumentBase
    {
        public PassportModel(string countryOfIssue, string surName, string givenNames, string passportNum, string dob, string expDate,
            string personalNum, string placeOfBirth, string nationality, string sex, string docType = "P<")
        {
            _countryOfIssue = countryOfIssue;
            _surName = surName;
            _givenNames = givenNames;
            _passportNum = passportNum;
            _dob = dob;
            _expDate = expDate;
            _personalNum = personalNum;
            _placeOfBirth = placeOfBirth;
            _nationality = nationality;
            _sex = sex;
            _docType = docType;
        }

        string _mrz = string.Empty;
        string _docType = "P<";
        string _countryOfIssue;
        string _surName = string.Empty;
        string _givenNames = string.Empty;

        string _passportNum = string.Empty;
        string _dob = string.Empty;
        string _expDate = string.Empty;
        string _personalNum = string.Empty;
        string _placeOfBirth = string.Empty;
        string _nationality;
        //SexCode _sex;
        string _sex;

        public string MRZ
        {
            get { return _mrz; }
            set { _mrz = value; }
        }
        public string DocType
        {
            get { return _docType; }
            set { _docType = value; }
        }
        public string CountryOfIssue
        {
            get { return _countryOfIssue; }
            set { _countryOfIssue = value; }
        }
        public string SurName
        {
            get { return _surName; }
            set { _surName = value; }
        }
        public string GivenNames
        {
            get { return _givenNames; }
            set { _givenNames = value; }
        }
        public string PassportNum
        {
            get { return _passportNum; }
            set { _passportNum = value; }
        }
        public string Dob
        {
            get { return _dob; }
            set { _dob = value; }
        }
        public string ExpDate
        {
            get { return _expDate; }
            set { _expDate = value; }
        }
        public string PersonalNum
        {
            get { return _personalNum; }
            set { _personalNum = value; }
        }
        public string Nationality
        {
            get { return _nationality; }
            set { _nationality = value; }
        }
        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        //public SexCode Sex
        //{
        //    get { return _sex; }
        //    set { _sex = value; }
        //}
    }
}
