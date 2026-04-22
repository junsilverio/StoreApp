import { useEffect, useState } from 'react';
import { Box, Typography, Chip, CircularProgress, Snackbar, Alert, MenuItem, Select, FormControl } from '@mui/material';
import { DataGrid, type GridColDef } from '@mui/x-data-grid';
import { getOrders, updateOrderStatus, type Order } from '../api/ordersApi';

const STATUS_MAP: Record<number, { label: string; color: 'warning' | 'info' | 'error' | 'success' }> = {
  1: { label: 'Pending', color: 'warning' },
  2: { label: 'Processing', color: 'info' },
  3: { label: 'Rejected', color: 'error' },
  4: { label: 'Completed', color: 'success' },
};

export default function DeliveryPage() {
  const [orders, setOrders] = useState<Order[]>([]);
  const [loading, setLoading] = useState(true);
  const [snack, setSnack] = useState({ open: false, msg: '', severity: 'success' as 'success' | 'error' });

  const load = () => {
    setLoading(true);
    getOrders()
      .then((res) => setOrders(res.data))
      .finally(() => setLoading(false));
  };

  useEffect(() => { load(); }, []);

  const handleStatusChange = async (id: number, status: number) => {
    try {
      await updateOrderStatus(id, status);
      setSnack({ open: true, msg: 'Order status updated', severity: 'success' });
      load();
    } catch {
      setSnack({ open: true, msg: 'Failed to update status', severity: 'error' });
    }
  };

  const columns: GridColDef[] = [
    { field: 'orderId', headerName: 'Order #', width: 100 },
    { field: 'customerId', headerName: 'Customer ID', width: 120 },
    { field: 'orderDate', headerName: 'Order Date', width: 150, valueFormatter: (value) => value ? new Date(value as string).toLocaleDateString() : '' },
    { field: 'requiredDate', headerName: 'Required By', width: 150, valueFormatter: (value) => value ? new Date(value as string).toLocaleDateString() : '' },
    { field: 'storeId', headerName: 'Store', width: 80 },
    {
      field: 'orderStatus',
      headerName: 'Status',
      width: 130,
      renderCell: (params) => {
        const s = STATUS_MAP[params.value as number] ?? { label: 'Unknown', color: 'default' as const };
        return <Chip label={s.label} color={s.color} size="small" />;
      },
    },
    {
      field: 'actions',
      headerName: 'Update Status',
      width: 160,
      sortable: false,
      renderCell: (params) => (
        <FormControl size="small" fullWidth>
          <Select
            value={params.row.orderStatus}
            onChange={(e) => handleStatusChange(params.row.id, Number(e.target.value))}
          >
            <MenuItem value={1}>Pending</MenuItem>
            <MenuItem value={2}>Processing</MenuItem>
            <MenuItem value={3}>Rejected</MenuItem>
            <MenuItem value={4}>Completed</MenuItem>
          </Select>
        </FormControl>
      ),
    },
  ];

  if (loading) return <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}><CircularProgress /></Box>;

  return (
    <Box>
      <Typography variant="h4" gutterBottom>Deliveries</Typography>
      <DataGrid
        rows={orders}
        columns={columns}
        pageSizeOptions={[10, 25, 50]}
        initialState={{ pagination: { paginationModel: { pageSize: 10 } } }}
        autoHeight
        disableRowSelectionOnClick
      />
      <Snackbar open={snack.open} autoHideDuration={3000} onClose={() => setSnack({ ...snack, open: false })}>
        <Alert severity={snack.severity}>{snack.msg}</Alert>
      </Snackbar>
    </Box>
  );
}
