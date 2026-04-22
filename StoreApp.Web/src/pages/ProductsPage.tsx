import { useEffect, useState } from 'react';
import {
  Box, Typography, Button, CircularProgress, Snackbar, Alert,
  Dialog, DialogTitle, DialogContent, DialogActions, TextField, MenuItem,
} from '@mui/material';
import { DataGrid, GridActionsCellItem, type GridColDef } from '@mui/x-data-grid';
import DeleteIcon from '@mui/icons-material/Delete';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';
import { getProducts, getBrands, getCategories, createProduct, updateProduct, deleteProduct, type Product, type Brand, type Category } from '../api/inventoryApi';

const emptyForm = { productName: '', brandId: 0, categoryId: 0, modelYear: new Date().getFullYear(), listPrice: 0 };

export default function ProductsPage() {
  const [products, setProducts] = useState<Product[]>([]);
  const [brands, setBrands] = useState<Brand[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editingProduct, setEditingProduct] = useState<Product | null>(null);
  const [form, setForm] = useState(emptyForm);
  const [snack, setSnack] = useState({ open: false, msg: '', severity: 'success' as 'success' | 'error' });

  const load = () => {
    setLoading(true);
    Promise.all([getProducts(), getBrands(), getCategories()])
      .then(([p, b, c]) => { setProducts(p.data); setBrands(b.data); setCategories(c.data); })
      .finally(() => setLoading(false));
  };

  useEffect(() => { load(); }, []);

  const openCreate = () => { setEditingProduct(null); setForm(emptyForm); setDialogOpen(true); };
  const openEdit = (p: Product) => {
    setEditingProduct(p);
    setForm({ productName: p.productName, brandId: p.brandId, categoryId: p.categoryId, modelYear: p.modelYear, listPrice: p.listPrice });
    setDialogOpen(true);
  };

  const handleSave = async () => {
    try {
      if (editingProduct) {
        await updateProduct(editingProduct.id, { ...editingProduct, ...form });
        setSnack({ open: true, msg: 'Product updated', severity: 'success' });
      } else {
        await createProduct(form);
        setSnack({ open: true, msg: 'Product created', severity: 'success' });
      }
      setDialogOpen(false);
      load();
    } catch {
      setSnack({ open: true, msg: 'Operation failed', severity: 'error' });
    }
  };

  const handleDelete = async (id: number) => {
    try {
      await deleteProduct(id);
      setSnack({ open: true, msg: 'Product deleted', severity: 'success' });
      load();
    } catch {
      setSnack({ open: true, msg: 'Delete failed', severity: 'error' });
    }
  };

  const getBrandName = (id: number) => brands.find((b) => b.brandId === id)?.brandName ?? id;
  const getCategoryName = (id: number) => categories.find((c) => c.categoryId === id)?.categoryName ?? id;

  const columns: GridColDef[] = [
    { field: 'productId', headerName: 'ID', width: 70 },
    { field: 'productName', headerName: 'Product Name', flex: 1 },
    { field: 'brandId', headerName: 'Brand', width: 150, valueFormatter: (value) => getBrandName(value as number) },
    { field: 'categoryId', headerName: 'Category', width: 150, valueFormatter: (value) => getCategoryName(value as number) },
    { field: 'modelYear', headerName: 'Year', width: 80 },
    { field: 'listPrice', headerName: 'Price', width: 100, valueFormatter: (value) => `$${Number(value).toFixed(2)}` },
    {
      field: 'actions',
      type: 'actions',
      headerName: 'Actions',
      width: 100,
      getActions: (params) => [
        <GridActionsCellItem icon={<EditIcon />} label="Edit" onClick={() => openEdit(params.row as Product)} />,
        <GridActionsCellItem icon={<DeleteIcon />} label="Delete" onClick={() => handleDelete(params.row.id)} />,
      ],
    },
  ];

  if (loading) return <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}><CircularProgress /></Box>;

  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 2 }}>
        <Typography variant="h4">Products</Typography>
        <Button variant="contained" startIcon={<AddIcon />} onClick={openCreate}>Add Product</Button>
      </Box>
      <DataGrid
        rows={products}
        columns={columns}
        pageSizeOptions={[10, 25, 50]}
        initialState={{ pagination: { paginationModel: { pageSize: 10 } } }}
        autoHeight
        disableRowSelectionOnClick
      />
      <Dialog open={dialogOpen} onClose={() => setDialogOpen(false)} maxWidth="sm" fullWidth>
        <DialogTitle>{editingProduct ? 'Edit Product' : 'Add Product'}</DialogTitle>
        <DialogContent sx={{ display: 'flex', flexDirection: 'column', gap: 2, pt: 2 }}>
          <TextField label="Product Name" value={form.productName} onChange={(e) => setForm({ ...form, productName: e.target.value })} fullWidth />
          <TextField select label="Brand" value={form.brandId} onChange={(e) => setForm({ ...form, brandId: Number(e.target.value) })} fullWidth>
            {brands.map((b) => <MenuItem key={b.brandId} value={b.brandId}>{b.brandName}</MenuItem>)}
          </TextField>
          <TextField select label="Category" value={form.categoryId} onChange={(e) => setForm({ ...form, categoryId: Number(e.target.value) })} fullWidth>
            {categories.map((c) => <MenuItem key={c.categoryId} value={c.categoryId}>{c.categoryName}</MenuItem>)}
          </TextField>
          <TextField label="Model Year" type="number" value={form.modelYear} onChange={(e) => setForm({ ...form, modelYear: Number(e.target.value) })} fullWidth />
          <TextField label="List Price" type="number" value={form.listPrice} onChange={(e) => setForm({ ...form, listPrice: Number(e.target.value) })} fullWidth />
        </DialogContent>
        <DialogActions>
          <Button onClick={() => setDialogOpen(false)}>Cancel</Button>
          <Button onClick={handleSave} variant="contained">Save</Button>
        </DialogActions>
      </Dialog>
      <Snackbar open={snack.open} autoHideDuration={3000} onClose={() => setSnack({ ...snack, open: false })}>
        <Alert severity={snack.severity}>{snack.msg}</Alert>
      </Snackbar>
    </Box>
  );
}
