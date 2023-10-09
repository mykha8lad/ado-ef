using ADO_EF_P12.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADO_EF_P12;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private DataContext dataContext;
    public ObservableCollection<Pair> Pairs { get; set; }

    public MainWindow()
    {
        InitializeComponent();
        dataContext = new();
        Pairs = new();
        this.DataContext = this;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        DepartmentsCountLabel.Content = dataContext.Departments
            .Count()
            .ToString();
        ManagersCountLabel.Content = dataContext.Managers
            .Count()
            .ToString();
        TopChiefCountLabel.Content =
                dataContext
                .Managers
                .Where(manager => manager.IdChief == null)                   
                .Count()
                .ToString();
        Subordinate.Content = dataContext.Managers
            .Where(manager => manager.IdChief != null)
            .Count()
            .ToString();        
        Guid itEmployee = Guid
            .Parse(dataContext.Departments
            .Where(department => department.Name == "IT відділ")
                                 .Select(department => department.Id)
                                 .First()
                                 .ToString());
        EmployeeIT.Content = dataContext.Managers
            .Where(manager => manager.IdSecDep == itEmployee || manager.IdMainDep == itEmployee)
            .Count()
            .ToString();

        EmployeeTwoDepartments.Content = dataContext.Managers
            .Where(manager => manager.IdMainDep != null && manager.IdSecDep != null)
            .Count()
            .ToString();
    }

    private void Button1_Click(object sender, RoutedEventArgs e)
    {
        var query = dataContext
               .Managers
               .Where(m => m.IdMainDep == Guid.Parse("131ef84b-f06e-494b-848f-bb4bc0604266"))
               .Select(                       
                   m =>                       
                   new Pair                   
                   {                          
                       Key = m.Surname,       
                       Value = $"{m.Name[0]}. {m.Secname[0]}."
                   }
               )
               .Take(7);
        
        

        Pairs.Clear();
        foreach (var pair in query)
        {                          
            Pairs.Add(pair);
        }
    }

    private void Button2_Click(object sender, RoutedEventArgs e)
    {
        var query = dataContext           
                .Managers                 
                .Join(                    
                    dataContext.Departments,
                    m => m.IdMainDep,       
                    d => d.Id,              
                    (m, d) =>               
                       new Pair             
                       {                    
                           Key = m.Surname, 
                           Value = d.Name   
                       }
                )     
                .Skip(3)
                .Take(7);                  

        Pairs.Clear();
        foreach (var pair in query)
        {
            Pairs.Add(pair);
        }
    }

    private void Button3_Click(object sender, RoutedEventArgs e)
    {
        var query = dataContext
                .Managers
                .Join(
                    dataContext.Managers,
                    m => m.IdChief,
                    chief => chief.Id,
                    (m, chief) => new Pair
                    {
                        Key = m.Surname,
                        Value = chief.Surname
                    }
                )
                .ToList()
                .Take(7)
                .OrderBy(pair => pair.Key);

        Pairs.Clear();
        foreach (var pair in query)
        {
            Pairs.Add(pair);
        }
    }

    private void Button4_Click(object sender, RoutedEventArgs e)
    {
        var query = dataContext
            .Managers
                .OrderByDescending(
                    m => m.CreateDt
                )
                .Select(
                    m => new Pair { Key = $"{m.CreateDt}",
                    Value = $"{m.Surname} {m.Name[0]}.{m.Secname[0]}." }
                )
                .Take(7);

        Pairs.Clear();
        foreach (var pair in query)
        {
            Pairs.Add(pair);
        }
    }

    private void Button5_Click(object sender, RoutedEventArgs e)
    {
        var query = dataContext
            .Managers
            .Join(
                dataContext.Departments,
                m => m.IdSecDep,
                d => d.Id,
                (m, d) => new Pair() { Key = $"{m.Surname} {m.Name[0]}.{m.Secname[0]}.", Value = d.Name }
            )
            .Take(7)
            .OrderBy(pair => pair.Value);

        Pairs.Clear();
        foreach (var pair in query)
        {
            Pairs.Add(pair);
        }
    }
}

public class Pair
{
    public String Key { get; set; } = null!;
    public String? Value { get; set; }
}
