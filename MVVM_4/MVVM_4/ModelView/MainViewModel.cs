using MVVM_4.Model;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using MVVM_4.ViewModel.Helpers;
using System.Collections.ObjectModel;

namespace MVVM_4.ModelView
{
    internal class MainViewModel : BindingHelper
    {
        #region Свойства
        public string date;
        public string Date
        {
            get { return date; }
            set { date = value; OnPropertyChanged(); }
        }
        public string name;
        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }
        public int selectIndex;
        public int SelectIndex
        {
            get { return selectIndex; }
            set { selectIndex = value; OnPropertyChanged(); }
        }
        public int selectIndex_datagrid;
        public int SelectIndex_datagrid
        {
            get { return selectIndex_datagrid; }
            set { selectIndex_datagrid = value; OnPropertyChanged(); }
        }
        public string itog;
        public string Itog
        {
            get { return itog; }
            set { itog = value; OnPropertyChanged(); }
        }
        public string money;
        public string Money
        {
            get { return money; }
            set { money = value; OnPropertyChanged(); }
        }
        public string type;
        public string Type
        {
            get { return type; }
            set { type = value; OnPropertyChanged(); }
        }
        public string new_type_text;
        public string New_type_text
        {
            get { return new_type_text; }
            set { new_type_text = value; OnPropertyChanged(); }
        }
        private ObservableCollection<ychet> spisok;
        public ObservableCollection<ychet> Spisok
        {
            get { return spisok; }
            set { spisok = value; OnPropertyChanged(); }
        }
        public ObservableCollection<string> types;
        public ObservableCollection<string> Types
        {
            get { return types; }
            set { types = value; OnPropertyChanged(); }
        }
        public BindableCommand Create_click { get; set; }
        public BindableCommand Add_type { get; set; }
        public BindableCommand Spisok_SelectionChanged { get; set; }
        public BindableCommand Apdata_click { get; set; }
        public BindableCommand Delete_click { get; set; }
        public BindableCommand Select_date { get; set; }
        public BindableCommand Add_type_click { get; set; }
        #endregion
        int sum = 0;
        public readonly string PATH = "учет.json";
        public MainViewModel()
        {
            Date = DateTime.Now.ToString();
            Create_click = new BindableCommand(_ => create_Click());
            Spisok_SelectionChanged = new BindableCommand(_ => spisok_SelectionChanged());
            Select_date = new BindableCommand(_ => Date_SelectedDateChanged());
            Add_type_click = new BindableCommand(_ => tip_button_Click());
            Add_type = new BindableCommand(_ => add_type());
            Delete_click = new BindableCommand(_ => delete_Click());
            Apdata_click = new BindableCommand(_ => apdata_Click());
            Create_click = new BindableCommand(_ => create_Click());
            FileIOService _fileIOService = new FileIOService("tip_combbox.json");
            Types = _fileIOService.Deserialize<ObservableCollection<string>>() ?? new ObservableCollection<string>();
            _fileIOService = new FileIOService(PATH);
            Spisok = _fileIOService.Deserialize<ObservableCollection<ychet>>() ?? new ObservableCollection<ychet>();
            Zagruzka();
        }

        public void Zagruzka()
        {
            Spisok.Clear();
            foreach (ychet task in new FileIOService(PATH).Deserialize<ObservableCollection<ychet>>())
            {
                if((string)task.Date.ToString().Remove(10) == date.Remove(10)) { Spisok.Add(task); }
            }
            Money_();
        }
        public void add_type()
        {
            FileIOService _fileIOService = new FileIOService("tip_combbox.json");
            if (new_type_text != "" && new_type_text != null)
            {
                Types.Add(new_type_text);
                _fileIOService.Serialize(Types);
            }
        }
        public void Money_()
        {
            sum = 0;
            foreach (ychet task in spisok)
            {
                if ((bool)task.Record) sum += (int)task.money;
                else sum -= (int)task.money;
            }
            Itog = sum.ToString();
        }

        public void create_Click()
        {
            if (name != "" && type != null && money != "")
            {
                ychet item;
                if (Convert.ToInt32(money) < 0)
                    item = new ychet(name, type, Convert.ToInt32(money) * -1, false, Date);
                else
                    item = new ychet(name, type, Convert.ToInt32(money), true, Date);
                Spisok.Add(item);
                Name = "";
                Type = "";
                Money = "";
                FileIOService _fileIOService = new FileIOService(PATH);
                _fileIOService.Serialize(spisok);
                Zagruzka();
            }
        }

        public void apdata_Click()
        {
            if (SelectIndex > -1)
            {
                if (name != "" && type != null && money != "")
                {
                    spisok[selectIndex].Name = name;
                    spisok[selectIndex].Tip = Type;
                    spisok[selectIndex].money = Convert.ToInt32(money);
                    if (Convert.ToInt32(money) < 0) spisok[selectIndex].Record = false;
                    else spisok[selectIndex].Record = true;
                    Name = "";
                    Type = "";
                    Money = "";
                    FileIOService _fileIOService = new FileIOService(PATH);
                    _fileIOService.Serialize(spisok);
                    Zagruzka();
                }
            }
        }

        public void delete_Click()
        {
            if (selectIndex > -1)
            {
                Name = "";
                Type = "";
                Money = "";
                Spisok.Remove(spisok[selectIndex]);
                FileIOService _fileIOService = new FileIOService(PATH);
                _fileIOService.Serialize(spisok);
                Zagruzka();
            }
        }

        public void tip_button_Click()
        {
            new new_type().Show();
        }

        public void Date_SelectedDateChanged() => Zagruzka();
        public void spisok_SelectionChanged()
        {
            if (selectIndex_datagrid > -1)
            {
                Name = spisok[selectIndex_datagrid].Name;
                Type = spisok[selectIndex_datagrid].Tip;
                if (!spisok[selectIndex_datagrid].Record) Money = (-1 * spisok[selectIndex_datagrid].money).ToString();
                else Money = spisok[selectIndex_datagrid].money.ToString();
            }
        }
    }
}
