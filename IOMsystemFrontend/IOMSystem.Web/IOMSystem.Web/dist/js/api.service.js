const API_BASE_URL = 'http://localhost:5102/api';

const ApiService = {
    async get(endpoint) {
        try {
            const response = await fetch(`${API_BASE_URL}/${endpoint}`);
            if (!response.ok) throw new Error(`API Error: ${response.status}`);
            return await response.json();
        } catch (error) {
            console.error('GET request failed:', error);
            throw error;
        }
    },

    async post(endpoint, data) {
        try {
            const response = await fetch(`${API_BASE_URL}/${endpoint}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
            if (!response.ok) {
                const text = await response.text();
                throw new Error(text || `API Error: ${response.status}`);
            }
            // Handle empty responses (e.g. 201 Created with no body)
            const text = await response.text();
            return text ? JSON.parse(text) : {};
        } catch (error) {
            console.error('POST request failed:', error);
            throw error;
        }
    },

    async put(endpoint, data) {
        try {
            const response = await fetch(`${API_BASE_URL}/${endpoint}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });
            if (!response.ok) throw new Error(`API Error: ${response.status}`);
            return true;
        } catch (error) {
            console.error('PUT request failed:', error);
            throw error;
        }
    },

    async delete(endpoint) {
        try {
            const response = await fetch(`${API_BASE_URL}/${endpoint}`, {
                method: 'DELETE'
            });
            if (!response.ok) throw new Error(`API Error: ${response.status}`);
            return true;
        } catch (error) {
            console.error('DELETE request failed:', error);
            throw error;
        }
    }
};

window.ApiService = ApiService;
