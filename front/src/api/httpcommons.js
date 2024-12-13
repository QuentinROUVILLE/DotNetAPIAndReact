const baseProtocol = "http://"
const baseURL = "localhost:8080";

export async function fetchAPI(url, options = {}) {
    const response = await fetch(baseProtocol + baseURL + url, {
        ...options,
        headers: {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${localStorage.getItem('token')}`,
            ...options.headers,
        },
    });

    if (response.status === 401) {
        handleUnauthorized();
        throw new Error("Unauthorized");
    }

    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }

    return response.json();
}

export function createWebSocket(url) {
    return new WebSocket(`ws://${baseURL}${url}`);
}

function handleUnauthorized() {
    localStorage.removeItem('token');
}