import axios, { AxiosInstance } from 'axios';

const instance: AxiosInstance = axios.create({
    baseURL: 'http://localhost:5088',
    headers: {
        'Content-Type': 'application/json',
    },
});

// Interceptor koji će se pozivati prije svakog zahtjeva
instance.interceptors.request.use((config) => {
    // Dodajte token u header ako je dostupan u localStorage-u
    const token = localStorage.getItem('token');
    if (token) {
        config.headers['Authorization'] = `Bearer ${token}`;
    }
    return config;
}, (error) => {
    return Promise.reject(error);
});

export default instance;
