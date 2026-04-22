import { useEffect, useState } from 'react';
import { Grid, Card, CardContent, Typography, CircularProgress, Box } from '@mui/material';
import { getProducts, getStocks } from '../api/inventoryApi';
import { getOrders } from '../api/ordersApi';

interface SummaryCard { label: string; value: number; color: string; }

export default function Dashboard() {
  const [cards, setCards] = useState<SummaryCard[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    Promise.all([getProducts(), getStocks(), getOrders()])
      .then(([products, stocks, orders]) => {
        const lowStock = stocks.data.filter((s) => s.quantity < 5).length;
        const pending = orders.data.filter((o) => o.orderStatus === 1).length;
        const completed = orders.data.filter((o) => o.orderStatus === 4).length;
        setCards([
          { label: 'Total Products', value: products.data.length, color: '#1976d2' },
          { label: 'Low Stock Items', value: lowStock, color: '#d32f2f' },
          { label: 'Pending Orders', value: pending, color: '#ed6c02' },
          { label: 'Completed Orders', value: completed, color: '#2e7d32' },
        ]);
      })
      .catch(() => setCards([]))
      .finally(() => setLoading(false));
  }, []);

  if (loading) return <Box sx={{ display: 'flex', justifyContent: 'center', mt: 4 }}><CircularProgress /></Box>;

  return (
    <Box>
      <Typography variant="h4" gutterBottom>Dashboard</Typography>
      <Grid container spacing={3}>
        {cards.map((card) => (
          <Grid size={{ xs: 12, sm: 6, md: 3 }} key={card.label}>
            <Card sx={{ borderLeft: `6px solid ${card.color}` }}>
              <CardContent>
                <Typography color="text.secondary" gutterBottom>{card.label}</Typography>
                <Typography variant="h3" sx={{ color: card.color }}>{card.value}</Typography>
              </CardContent>
            </Card>
          </Grid>
        ))}
      </Grid>
    </Box>
  );
}
