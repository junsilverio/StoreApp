import { useEffect, useState } from 'react';
import {
  Box, Typography, Chip, CircularProgress, Snackbar, Alert,
  Dialog, DialogTitle, DialogContent, DialogActions, Button, TextField,
} from '@mui/material';
import { DataGrid, type GridColDef } from '@mui/x-data-grid';
import { getProducts, getStocks, getBrands, getCategories, updateStock, type Product, type Stock, type Brand, type Category } from '../api/inventoryApi';

export default function InventoryPage() {
  const [products, setProducts] = useState<Product[]>([]);
  const [stocks, setStocks] = useState<Stock[]>([]);
  const [brands, setBrands] = useState<Brand[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [snack, setSnack] = useState({ open: false, msg: '', severity: 'success' as 'success' | 'error' });
  const [editStock, setEditStock] = useState<Stock | null>(null);
  const [newQty, setNewQty] = useState('');

  const load = () => {
    setLoading(true);
    Promise.all([getProducts(), getStocks(), getBrands(), getCategories()])
      .then(([p, s, b, c]) => {
        setProducts(p.data);
        setStocks(s.data);
        setBrands(b.data);
        setCategories(c.data);
      })
      .finally(() => setLoading(false));
  };

  useEffect(() => { load(); }, []);

  const getBrandName = (id: number) => brands.find((b) => b.brandId === id)?.brandName ?? id;
  const getCategoryName = (id: number) => categories.find((c) => c.categoryId === id)?.categoryName ?? id;
  const getQuantity = (productId: number) =>
    stocks.filter((s) => s.productId === productId).reduce((sum, s) => sum + s.quantity, 0);

  const rows = products.map((p) => ({
    ...p,
    brandName: getBrandName(p.brandId),
    categoryName: getCategoryName(p.categoryId),
    totalStock: getQuantity(p.productId),
  }));

  const columns: GridColDef[] = [
    { field: 'productId', headerName: 'ID', width: 70 },
    { field: 'productName', headerName: 'Product', flex: 1 },
    { field: 'brandName', headerName: 'Brand', width: 150 },
    { field: 'categoryName', headerName: 'Category', width: 150 },
    { field: 'modelYear', headerName: 'Year', width: 80 },
    { field: 'listPrice', headerName: 'Price', width: 100, valueFormatter: (value) => `$${Number(value).toFixed(2)}` },
    {
      field: 'totalStock',
      headerName: 'Stock',
      width: 120,
      renderCell: (params) => (
        <Chip
          label={params.value}
          color={params.value < 5 ? 'error' : 'success'}
          size="small"
        />
      ),
    },
  ];

  const handleSaveStock = async () => {
    if (!editStock) return;
    try {
      await updateStock({ ...editStock, quantity: parseInt(newQty, 10) });
      setSnack({ open: true, msg: 'Stock updated', severity: 'success' });
      setEditStock(null);
      load();
    } catch {
      setSnack({ open: true, msg: 'Failed to update stock', severity: 'error' });
    }
  };

  if (loading) return <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}><CircularProgress /></Box>;

  return (
    <Box>
      <Typography variant="h4" gutterBottom>Inventory</Typography>
      <DataGrid
        rows={rows}
        columns={columns}
        pageSizeOptions={[10, 25, 50]}
        initialState={{ pagination: { paginationModel: { pageSize: 10 } } }}
        autoHeight
        disableRowSelectionOnClick
      />
      <Dialog open={!!editStock} onClose={() => setEditStock(null)}>
        <DialogTitle>Update Stock Quantity</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            label="Quantity"
            type="number"
            value={newQty}
            onChange={(e) => setNewQty(e.target.value)}
            fullWidth
            margin="dense"
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setEditStock(null)}>Cancel</Button>
          <Button onClick={handleSaveStock} variant="contained">Save</Button>
        </DialogActions>
      </Dialog>
      <Snackbar open={snack.open} autoHideDuration={3000} onClose={() => setSnack({ ...snack, open: false })}>
        <Alert severity={snack.severity}>{snack.msg}</Alert>
      </Snackbar>
    </Box>
  );
}
