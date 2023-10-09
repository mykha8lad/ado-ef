﻿using ADO_EF_P12.Data;
using Microsoft.EntityFrameworkCore;
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
                    m => new Pair
                    {
                        Key = $"{m.CreateDt}",
                        Value = $"{m.Surname} {m.Name[0]}.{m.Secname[0]}."
                    }
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

    #region Генератор - збільшується при кожному зверненні
    private int _N;
    public int N { get => _N++; set => _N = value; }
    #endregion
    private void Button6_Click(object sender, RoutedEventArgs e)
    {
        N = 1;
        var query = dataContext
                .Departments
                .OrderBy(d => d.Name)
                .Select(d => new Pair()
                {
                    Key = (N).ToString(),
                    Value = d.Name
                });

        Pairs.Clear();
        foreach (var pair in query)
        {
            Pairs.Add(pair);
        }
    }

    private void Button7_Click(object sender, RoutedEventArgs e)
    {
        N = 1;
        var query = dataContext
            .Departments
            .OrderBy(d => d.Name)
            .AsEnumerable()
            .Select(d => new Pair()
            {
                Key = (N).ToString(),
                Value = d.Name
            });

        Pairs.Clear();
        foreach (var pair in query)
        {
            Pairs.Add(pair);
        }
    }

    private void Button8_Click(object sender, RoutedEventArgs e)
    {
        var query = dataContext
                .Departments
                .GroupJoin(
                    dataContext.Managers,
                    d => d.Id,
                    m => m.IdMainDep,
                    (dep, mans) => new Pair
                    {
                        Key = dep.Name,
                        Value = mans.Count()
                                .ToString()
                    }
                );

        Pairs.Clear();
        foreach (var pair in query)
        {
            Pairs.Add(pair);
        }
    }

    private void Button9_Click(object sender, RoutedEventArgs e)
    {
        var query = dataContext.Managers
                .GroupJoin(
                    dataContext.Managers,
                    chef => chef.Id,
                    sub => sub.IdChief,
                    (chef, subs) => new Pair()
                    {
                        Key = $"{chef.Surname} {chef.Name[0]}. {chef.Secname[0]}.",
                        Value = subs.Count().ToString()
                    }
                )
                .Where(p => Convert.ToInt32(p.Value) > 0);

        Pairs.Clear();
        foreach (var pair in query)
        {
            Pairs.Add(pair);
        }
    }

    private void Button10_Click(object sender, RoutedEventArgs e)
    {
        var query = dataContext.Managers
                .GroupBy(m => m.Surname)
                .Select(group => new Pair
                {
                    Key = group.Key,
                    Value = group.Count().ToString()
                })
                .Where(p => Convert.ToInt32(p.Value) > 1);

        Pairs.Clear();
        foreach (var pair in query)
        {
            Pairs.Add(pair);
        }
    }

    private void Button11_Click(object sender, RoutedEventArgs e)
    {
        var query = dataContext
            .Departments
            .GroupJoin(
                dataContext.Managers,
                d => d.Id,
                m => m.IdSecDep,
            (d, m) => new Pair
            {
                Key = d.Name,
                Value = m.Count().ToString()
            })
            .OrderByDescending(pair => Convert.ToInt32(pair.Value));

        Pairs.Clear();
        foreach (var pair in query)
        {
            Pairs.Add(pair);
        }
    }

    private void Button12_Click(object sender, RoutedEventArgs e)
    {
        N = 1;

        var query = dataContext
            .Managers
            .GroupBy(m => m.Surname)
            .AsEnumerable()
            .Where(m => m.Count() > 1)
            .Select(
                group => new Pair 
                {
                    Key = N.ToString(),
                    Value = group.Key
                });

        Pairs.Clear();
        foreach (var pair in query)
        {
            Pairs.Add(pair);
        }
    }

    private void Button13_Click(object sender, RoutedEventArgs e)
    {
        var query = dataContext
            .Managers
            .GroupJoin(
                dataContext.Managers,
                chief => chief.Id,
                m => m.IdChief,
                (chief, m) => new Pair
                {
                    Key = m.Count().ToString(),
                    Value = $"{chief.Surname} {chief.Name[0]}.{chief.Secname[0]}."
                })
                .OrderByDescending(pair => Convert.ToInt32(pair.Key))
                .Take(3);

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
