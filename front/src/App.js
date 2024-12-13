import React, { useState, useEffect } from 'react';
import LoginForm from './components/LoginForm';
import ServicesList from './components/ServiceList';
import ServiceActions from './components/ServiceActions';
import { loginRequest } from './api/services/Authentication';
import { fetchServices, fetchActions, runServiceAction, getLogs } from './api/services/Service';
import './App.css'

function App() {
    const [token, setToken] = useState(localStorage.getItem('token'));
    const [services, setServices] = useState([]);
    const [selectedService, setSelectedService] = useState(null);
    const [actions, setActions] = useState([]);
    const [logs, setLogs] = useState("");

    useEffect(() => {
        if (token) {
            fetchServices(token)
                .then(data => setServices(data));
        }
    }, [token]);

    const login = (login, password) => {
        loginRequest(login, password)
            .then(data => {
                setToken(data.token);
            });
    };

    const logout = () => {
        setToken(null);
        localStorage.setItem('token', null);
    }

    const selectService = (selectedService) => {
        setSelectedService(selectedService);
        fetchActions(selectedService)
            .then(data => setActions(data));
    };

    const runAction = (action) => {
        runServiceAction(selectedService, action)
            .then(data => {
                const webSocket = getLogs(data.taskId);
                webSocket.onmessage = (event) => {
                    setLogs(logs => logs + event.data);
                };
            });
    };

    return (
        <div id="app">
            {!token ? (
                <LoginForm onLogin={login} />
            ) : (
                <div id="services">
                    <div id="serviceMenu">
                        <ServicesList services={services} onSelect={selectService}/>
                        <button onClick={() => logout()}>Se déconnecter</button>
                    </div>
                        <ServiceActions service={selectedService} actions={actions} onRunAction={runAction} logs={logs} />
                    
                </div>
            )}
        </div>
    );
}

export default App;
