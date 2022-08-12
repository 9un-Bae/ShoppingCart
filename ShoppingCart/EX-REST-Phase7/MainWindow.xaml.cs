using MyApp;
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

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<ShoppingCart> _carts;
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        async void LoadData()
        {
            _carts = await Shopping.GetAllCarts();
            lbCarts.ItemsSource = _carts;
        }

        private void btnUpdateCart_Click(object sender, RoutedEventArgs e)
        {
            if (lbCarts.SelectedValue != null) {
                CartWindow cartWindow = new CartWindow();
                cartWindow.LoadData((ShoppingCart)lbCarts.SelectedValue, CartWindow.CartMode.Updating);
                cartWindow.ShowDialog();
            }
            else
                MessageBox.Show("Please select a cart");
        }

        private void btnAddCart_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new CartWindow();
            ShoppingCart newCart = new ShoppingCart()
            {
                CartNumber = _carts.Count > 0 ? _carts.Max(c => c.CartNumber) + 1 : 1,
                CustomerName = "",
                Items=new List<CartItem>()
            };

            cartWindow.LoadData(newCart, CartWindow.CartMode.Adding);
            cartWindow.ShowDialog();
            LoadData();
            lbCarts.Items.Refresh();
        }

        private async void btnDeleteCart_Click(object sender, RoutedEventArgs e)
        {
            if (lbCarts.SelectedItem != null)
            {
                await Shopping.DeleteCart(((ShoppingCart)lbCarts.SelectedValue).CartNumber);
                LoadData();
                lbCarts.Items.Refresh();
            }
            else
                MessageBox.Show("Please select which cart to delete");
        }
    }
}
