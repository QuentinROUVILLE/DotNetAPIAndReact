import React from 'react';

export default function ServiceActions({ service, actions, onRunAction, logs }) {
    if (!service) return (
        <div id="actionPlaceHolder">
            <h3>Sélectionnez un service</h3>
        </div>
    );

    return (
        <div id="serviceActions">
            <div id="actionHeader">
                <div id="actionName">
                    <h2>{service}</h2>
                </div>
                <div id="actionButtons">
                {actions.map(action => (
                        <button key={action} onClick={()=>onRunAction(action)}>{action}</button>
                    ))}
                </div>
            </div>
            <div id="logs">
                <pre>{logs}</pre>
            </div>
        </div>
    );
}
