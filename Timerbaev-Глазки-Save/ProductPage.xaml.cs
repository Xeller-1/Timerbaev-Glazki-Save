﻿using System;
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

namespace Timerbaev_Глазки_Save
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private Agent currentAgent = new Agent();
        public ProductPage(Agent agent)
        {
            InitializeComponent();
            currentAgent = agent;
            var currentSales = Timerbaev_agentEntities.GetContext().ProductSale.ToList();
            currentSales = currentSales.Where(p => p.AgentID == currentAgent.ID).ToList();
            AgentSaleListView.ItemsSource = currentSales;
            UpdateSales();
        }

        private void AddSale_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddSalesPage(currentAgent as Agent));
            UpdateSales();
        }

        public void UpdateSales()
        {
            var currentProduct = Timerbaev_agentEntities.GetContext().ProductSale.ToList();
            AgentSaleListView.ItemsSource = currentProduct.Where(p => p.AgentID == currentAgent.ID);
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateSales();
        }

        private void DeleteSales_Click(object sender, RoutedEventArgs e)
        {
            var currentSale = (sender as Button).DataContext as ProductSale;
            var currentSalesList = Timerbaev_agentEntities.GetContext().ProductSale.ToList();
            currentSalesList = currentSalesList.Where(p => p.ID == currentSale.ID).ToList();
            if (currentSalesList.Count != 0)
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        Timerbaev_agentEntities.GetContext().ProductSale.Remove(currentSale);
                        Timerbaev_agentEntities.GetContext().SaveChanges();
                        UpdateSales();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }
    }
}
