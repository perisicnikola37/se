import axios, { AxiosInstance } from 'axios';
const token = localStorage.getItem('token');

const instance: AxiosInstance = axios.create({
    baseURL: 'http://localhost:5088',
    headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`,
    },
});

console.log(token);

export default instance;
