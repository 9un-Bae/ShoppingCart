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
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private ShoppingCart _cart;
        public enum CartMode { Adding, Updating};
        private CartMode _CurrentCartMode;

        public CartWindow()
        {
            InitializeComponent();
        }

        public void LoadData(ShoppingCart cart, CartMode cartMode)
        {
            _cart = cart;
            grdCart.DataContext = _cart;
            lbCartItems.ItemsSource = _cart.Items;
            _CurrentCartMode = cartMode;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (btnSave != null)
                btnSave.IsEnabled = true;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_CurrentCartMode == CartMode.Updating)
                await Shopping.UpdateCart(_cart);
            else
                await Shopping.AddCart(_cart);
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var currentMaxItemNumber = _cart.Items.Count >0 ? _cart.Items.Max(i => i.ItemNumber) :0;
            _cart.Items.Add(new CartItem() { ItemNumber = currentMaxItemNumber + 1 });
            lbCartItems.Items.Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lbCartItems.SelectedItem != null)
            {
                _cart.Items.Remove((CartItem)lbCartItems.SelectedValue);
                lbCartItems.Items.Refresh();
            }
            else
                MessageBox.Show("Please select which item to delete");
        }
    }
}
