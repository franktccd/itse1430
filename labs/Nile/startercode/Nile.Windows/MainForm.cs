/*
 * ITSE 1430
 * Frank Rygiewicz
 * 4/21/20
 */
using System;
using System.Configuration;
using System.Windows.Forms;
using Nile.Stores;
using Nile.Stores.Sql;

namespace Nile.Windows
{
    public partial class MainForm : Form
    {
        #region Construction

        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            var connString = ConfigurationManager.ConnectionStrings["ProductDatabase"];
            _database = new SqlProductDatabase(connString.ConnectionString);

            _gridProducts.AutoGenerateColumns = false;

            UpdateList();
        }

        #region Event Handlers
        
        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnProductAdd( object sender, EventArgs e )
        {
            var child = new ProductDetailForm("Product Details");
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //Handle errors
            try
            {
                //Save product
                _database.Add(child.Product);
            } catch (Exception ex)
            {
                DisplayError(ex.Message);
            }
            
            UpdateList();
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            var product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show("No products available.");
                return;
            };
            try
            {
                ObjectValidator.Validate(product);
                EditProduct(product);
            } catch (Exception ex)
            {
                DisplayError(ex.Message);
            }
        }        

        private void OnProductDelete( object sender, EventArgs e )
        {
            var product = GetSelectedProduct();
            if (product == null)
            {
                DisplayError("Product does not exist.");
                return;
            }
            DeleteProduct(product);
        }        
                
        private void OnEditRow( object sender, DataGridViewCellEventArgs e )
        {
            var grid = sender as DataGridView;

            //Handle column clicks
            if (e.RowIndex < 0)
                return;

            var row = grid.Rows[e.RowIndex];
            var item = row.DataBoundItem as Product;

            if (item != null)
                EditProduct(item);
        }

        private void OnKeyDownGrid( object sender, KeyEventArgs e )
        {
            if (e.KeyCode != Keys.Delete)
                return;

            var product = GetSelectedProduct();
            if (product != null)
                DeleteProduct(product);
			
			//Don't continue with key
            e.SuppressKeyPress = true;
        }

        private void DisplayError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK);
        }

        #endregion

        #region Private Members

        private void DeleteProduct ( Product product )
        {
            //Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{product.Name}'?",
                                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //Handle errors
            if (product.Id < 0)
                throw new ArgumentOutOfRangeException(nameof(product.Id), "Invalid product ID.");

            //Delete product
            _database.Remove(product.Id);
            UpdateList();
        }

        private void EditProduct ( Product product )
        {
            var child = new ProductDetailForm("Product Details");
            child.Product = product;
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //Handle errors
            try
            {
                //Save product
                _database.Update(child.Product);
            } catch (Exception ex)
            {
                DisplayError(ex.Message);
            }
            UpdateList();
        }

        private Product GetSelectedProduct ()
        {
            if (_gridProducts.SelectedRows.Count > 0)
                return _gridProducts.SelectedRows[0].DataBoundItem as Product;

            return null;
        }

        private void UpdateList ()
        {
            //Handle errors
            try
            {
                _bsProducts.DataSource = _database.GetAll();
            } catch (Exception ex)
            {
                DisplayError(ex.Message);
            }
        }

        private IProductDatabase _database;
        #endregion

        private void onHelpAbout ( object sender, EventArgs e )
        {
            var aboutBox = new AboutBox();
            aboutBox.ShowDialog(this);
        }
    }
}
