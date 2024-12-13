import React from 'react';

export default function ServicesList({ services, onSelect }) {
    return (
        <div id="serviceList">
            <h1>Services</h1>
            <ul>
                {services.map(service => (
                    <li key={service} onClick={()=>onSelect(service)}>
                        {service}
                    </li>
                ))}
            </ul>
        </div>
    );
}
