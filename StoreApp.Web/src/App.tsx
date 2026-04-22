import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Layout from './components/Layout';
import Dashboard from './pages/Dashboard';
import InventoryPage from './pages/InventoryPage';
import DeliveryPage from './pages/DeliveryPage';
import ProductsPage from './pages/ProductsPage';
import CustomersPage from './pages/CustomersPage';

function App() {
  return (
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path="/" element={<Dashboard />} />
          <Route path="/inventory" element={<InventoryPage />} />
          <Route path="/delivery" element={<DeliveryPage />} />
          <Route path="/products" element={<ProductsPage />} />
          <Route path="/customers" element={<CustomersPage />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  );
}

export default App;
