using ADO_EF_P12.Data;
using System;
using System.Collections.Generic;
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

    public MainWindow()
    {
        InitializeComponent();
        dataContext = new();
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
}
