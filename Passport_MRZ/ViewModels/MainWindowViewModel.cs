using Passport_MRZ.Base;
using Passport_MRZ.Helper;
using Passport_MRZ.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static Passport_MRZ.Models.DocumentBase;
using ISO3166;
using System.IO;

namespace Passport_MRZ.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _titlebar = "Prism Application";
        public string Titlebar
        {
            get { return _titlebar; }
            set { SetProperty(ref _titlebar, value); }
        }
        readonly EnumListItemCollection<Countries> countries = new EnumListItemCollection<Countries>();
        public ICommand DeleteJobCommand { get { return new DelegateCommand(this.testcommand); } }
        public DelegateCommand test2 { get; }
        public MainWindowViewModel()
        {
            test2 = new DelegateCommand(testcommand);
            var iso = ISO3166.Country.List;
            string asd = @"" + Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\user.jpg";
            DisplayedImage = asd;
            for (int i = 0; i < iso.Length; i++)
            {
                LstcountriesModels.Add(new CountriesModel
                {
                    CountriesName = iso[i].Name,
                    NumericCode = iso[i].NumericCode,
                    TwoLetterCode = iso[i].TwoLetterCode,
                    ThreeLetterCode = iso[i].ThreeLetterCode,
                    NameAndCode = iso[i].ThreeLetterCode + " : " + iso[i].Name
                });
                LstNationalityModels.Add(new CountriesModel
                {
                    CountriesName = iso[i].Name,
                    NumericCode = iso[i].NumericCode,
                    TwoLetterCode = iso[i].TwoLetterCode,
                    ThreeLetterCode = iso[i].ThreeLetterCode,
                    NameAndCode = iso[i].ThreeLetterCode + " : " + iso[i].Name
                });
                

            }
            LstGenderModels.Add(new Gender
            {
                GenderCode = "M",
                GenderType = "Male",
                GenderTypeAndCode = "M : Male"
            });
            LstGenderModels.Add(new Gender
            {
                GenderCode = "F",
                GenderType = "Female",
                GenderTypeAndCode = "F : Female"
            });
            LstGenderModels.Add(new Gender
            {
                GenderCode = "U",
                GenderType = "Unknown",
                GenderTypeAndCode = "U : Unknown"
            });

        }
        private void GenMRZ()
        {

            PassportModel tmp = new PassportModel(
                CountryCode,
                charRemap(Surname.Replace(' ', '<')),
                charRemap(Name.Replace(' ', '<')),
                PassportNo,
                Dateofbirth,
                Dateofexpiry,
                charRemap(Identification.Replace(' ', '<')),
                Dateofissue,
                Nationality,
                GenderCode,
                Type
                );
            MRZ = MRZCheckDigitAndCalculator.GeneratePassportMRZ(tmp);

        }
        private void GenIdentityMRZ()
        {

            IdentityDocumentModel tmp = new IdentityDocumentModel(
                CountryCode,
               // "UTO",
                charRemap(Surname.Replace(' ', '<')),
                charRemap(Name.Replace(' ', '<')),
                PassportNo,
                Dateofbirth,
                Dateofexpiry,
                charRemap(Identification.Replace(' ', '<')),
                charRemap(Optional2.Replace(' ', '<')),
                Dateofissue,
                Nationality,
                //"UTO",
                GenderCode,
                Type
                );

            VIZ = MRZCheckDigitAndCalculator.GenerateIdentityCardMRZ(tmp);

        }
        private static string charRemap(string input)
        {
            string output = input
            .Replace("Ä", "AE")
            .Replace("Å", "AA")
            .Replace("Æ", "AE")
            .Replace("Ĳ", "IJ")
            .Replace("Ñ", "N")
            .Replace("Ä", "AE")
            .Replace("Ö", "OE")
            .Replace("Ø", "OE")
            .Replace("Ü", "UE")
            .Replace("ß", "SS");
            return output;
        }
        private List<CountriesModel> _lstNationalityModels = new List<CountriesModel>();
        public List<CountriesModel> LstNationalityModels
        {
            get { return _lstNationalityModels; }
            set
            {
                SetProperty(ref _lstNationalityModels, value);
                NotifyPropertyChanged("LstNationalityModels");
            }
        }
        private List<CountriesModel> _lstcountriesModels = new List<CountriesModel>();
        public List<CountriesModel> LstcountriesModels
        {
            get { return _lstcountriesModels; }
            set
            {
                SetProperty(ref _lstcountriesModels, value);
                NotifyPropertyChanged("LstcountriesModels");
            }
        }
        private List<Gender> _lstGenderModels = new List<Gender>();
        public List<Gender> LstGenderModels
        {
            get { return _lstGenderModels; }
            set
            {
                SetProperty(ref _lstGenderModels, value);
                NotifyPropertyChanged("LstGenderModels");
            }
        }
        public EnumListItemCollection<Countries> Countries
        {
            get
            {
                return countries;
            }
        }

        Countries country;

        public Countries Country
        {
            get
            {
                return country;
            }
            set
            {
                SetProperty(ref country, value);
                Surname = country.ToString();
            }
        }


        private void testcommand()
        {
            MessageBox.Show("asdasd");

        }

        private CountriesModel _selectedCountries;
        public CountriesModel SelectedCountries
        {

            get { return _selectedCountries; }
            set
            {
                SetProperty(ref _selectedCountries, value);
                NotifyPropertyChanged("SelectedCountries");

                if (_selectedCountries != null)
                {
                    CountryCode = _selectedCountries.ThreeLetterCode;
                }

            }
        }
        private CountriesModel _selectedNationality;
        public CountriesModel SelectedNationality
        {

            get { return _selectedNationality; }
            set
            {
                SetProperty(ref _selectedNationality, value);
                NotifyPropertyChanged("SelectedNationality");

                if (_selectedNationality != null)
                {
                    Nationality = _selectedNationality.ThreeLetterCode;
                }

            }
        }
        private Gender _selectedGender;
        public Gender SelectedGender
        {

            get { return _selectedGender; }
            set
            {
                SetProperty(ref _selectedGender, value);
                NotifyPropertyChanged("SelectedGender");

                if (_selectedGender != null)
                {
                    GenderCode = _selectedGender.GenderCode;
                }

            }
        }
        private string resultMRZ()
        {
            string result = Type + CountryCode + PassportNo + Surname + Title + NameinThai + Nationality
                + Dateofbirth + Identification + GenderCode + Height + Placeofbrith + Dateofissue
                + IssueAuthority + Dateofexpiry + Holder;

            return result;
        }

        private string _type = string.Empty;
        public string Type
        {
            get { return _type; }
            set
            {
                SetProperty(ref _type, value);
                NotifyPropertyChanged("Type");
                GenMRZ();
                GenIdentityMRZ();
            }
        }
        private string _countryCode = string.Empty;
        public string CountryCode
        {
            get { return _countryCode; }
            set { SetProperty(ref _countryCode, value); NotifyPropertyChanged("CountryCode"); GenMRZ(); GenIdentityMRZ(); }
        }
        private string _passportNo = string.Empty;
        public string PassportNo
        {
            get { return _passportNo; }
            set { SetProperty(ref _passportNo, value); NotifyPropertyChanged("PassportNo"); GenMRZ(); GenIdentityMRZ(); }
        }
        private string _surname = string.Empty;
        public string Surname
        {
            get { return _surname; }
            set { SetProperty(ref _surname, value); NotifyPropertyChanged("Surname"); GenMRZ(); GenIdentityMRZ(); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); NotifyPropertyChanged("Title"); GenMRZ(); GenIdentityMRZ(); }
        }
        private string _name = string.Empty;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); NotifyPropertyChanged("Name"); GenMRZ(); GenIdentityMRZ(); }
        }

        private string _nameinThai = string.Empty;
        public string NameinThai
        {
            get { return _nameinThai; }
            set { SetProperty(ref _nameinThai, value); NotifyPropertyChanged("NameinThai"); GenMRZ(); GenIdentityMRZ(); }
        }

        private string _nationality = string.Empty;
        public string Nationality
        {
            get { return _nationality; }
            set { SetProperty(ref _nationality, value); NotifyPropertyChanged("Nationality"); GenMRZ(); GenIdentityMRZ(); }
        }

        private DateTime? _selectdateofbirth;
        public DateTime? SelectDateofbirth
        {
            get { return _selectdateofbirth; }
            set
            {
                SetProperty(ref _selectdateofbirth, value); NotifyPropertyChanged("SelectDateofbirth");
                if (_selectdateofbirth != null)
                {
                    Dateofbirth = _selectdateofbirth?.ToString("yyMMdd");
                }
            }
        }

        private string _dateofbirth = string.Empty;
        public string Dateofbirth
        {
            get { return _dateofbirth; }
            set
            {
                SetProperty(ref _dateofbirth, value); NotifyPropertyChanged("Dateofbirth");
                GenMRZ();
                GenIdentityMRZ();
            }
        }

        private string _identification = string.Empty;
        public string Identification
        {
            get { return _identification; }
            set { SetProperty(ref _identification, value); NotifyPropertyChanged("Identification"); GenMRZ(); GenIdentityMRZ(); }
        }
        private string _optional2 = string.Empty;
        public string Optional2
        {
            get { return _optional2; }
            set { SetProperty(ref _identification, value); NotifyPropertyChanged("Optional2"); GenMRZ(); GenIdentityMRZ(); }
        }

        private string _displayedImage = string.Empty;
        public string DisplayedImage
        {
            get { return _displayedImage; }
            set
            {
                SetProperty(ref _displayedImage, value); NotifyPropertyChanged("DisplayedImage");
            }
        }
        
        private string _genderCode = string.Empty;
        public string GenderCode
        {
            get { return _genderCode; }
            set { SetProperty(ref _genderCode, value); NotifyPropertyChanged("GenderCode"); GenMRZ(); GenIdentityMRZ(); }
        }

        private string _height = string.Empty;
        public string Height
        {
            get { return _height; }
            set { SetProperty(ref _height, value); NotifyPropertyChanged("Height"); GenMRZ(); GenIdentityMRZ(); }
        }

        private string _placeofbrith = string.Empty;
        public string Placeofbrith
        {
            get { return _placeofbrith; }
            set { SetProperty(ref _placeofbrith, value); NotifyPropertyChanged("Placeofbrith"); GenMRZ(); GenIdentityMRZ(); }
        }

        private DateTime _selectdateofissue = DateTime.Now;
        public DateTime SelectDateofissue
        {
            get { return _selectdateofissue; }
            set
            {
                SetProperty(ref _selectdateofissue, value); NotifyPropertyChanged("SelectDateofissue");
                if (_selectdateofissue != null)
                {
                    Dateofissue = _selectdateofissue.ToString("yyMMdd");
                }
            }
        }

        private string _dateofissue = string.Empty;
        public string Dateofissue
        {
            get { return _dateofissue; }
            set { SetProperty(ref _dateofissue, value); NotifyPropertyChanged("Dateofissue"); GenMRZ(); GenIdentityMRZ(); }
        }

        private string _issueAuthority = string.Empty;
        public string IssueAuthority
        {
            get { return _issueAuthority; }
            set { SetProperty(ref _issueAuthority, value); NotifyPropertyChanged("IssueAuthority"); GenMRZ(); GenIdentityMRZ(); }
        }

        private DateTime _selectdateofexpiry = DateTime.Now;
        public DateTime SelectDateofexpiry
        {
            get { return _selectdateofexpiry; }
            set
            {
                SetProperty(ref _selectdateofexpiry, value); NotifyPropertyChanged("SelectDateofexpiry");
                if (_selectdateofexpiry != null)
                {
                    Dateofexpiry = _selectdateofexpiry.ToString("yyMMdd");
                }
            }
        }

        private string _dateofexpiry = string.Empty;
        public string Dateofexpiry
        {
            get { return _dateofexpiry; }
            set { SetProperty(ref _dateofexpiry, value); NotifyPropertyChanged("Dateofexpiry"); GenMRZ(); GenIdentityMRZ(); }
        }

        private string _holder = string.Empty;
        public string Holder
        {
            get { return _holder; }
            set { SetProperty(ref _holder, value); NotifyPropertyChanged("Holder"); GenMRZ(); GenIdentityMRZ(); }
        }

        private string _mRZ = string.Empty;
        public string MRZ
        {
            get { return _mRZ; }
            set
            {
                SetProperty(ref _mRZ, value);
                NotifyPropertyChanged("MRZ");
            }
        }
        private string _vIZ = string.Empty;
        public string VIZ
        {
            get { return _vIZ; }
            set
            {
                SetProperty(ref _vIZ, value);
                NotifyPropertyChanged("VIZ");
            }
        }

        private string _mRZLine1 = string.Empty;
        public string MRZLine1
        {
            get { return _mRZLine1; }
            set
            {
                SetProperty(ref _mRZLine1, value);
                NotifyPropertyChanged("_mRZLine1");
            }
        }
        private string _mRZLine2 = string.Empty;
        public string MRZLine2
        {
            get { return _mRZLine2; }
            set
            {
                SetProperty(ref _mRZLine2, value);
                NotifyPropertyChanged("MRZLine2");
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
