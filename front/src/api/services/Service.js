import {createWebSocket, fetchAPI} from "../httpcommons";

export async function fetchServices() {
    return await fetchAPI(`/api/v1/services`);
}

export async function fetchActions(service) {
    return await fetchAPI(`/api/v1/services/${service}`);
}

export async function runServiceAction(service, action) {
    return await fetchAPI(`/api/v1/services/${service}`, {
        method: 'POST',
        body: JSON.stringify({ action })
    });
}

export function getLogs(taskId) {
    return createWebSocket(`/api/v1/ws/${taskId}`);
}
