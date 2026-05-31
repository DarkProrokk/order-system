import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    vus: 10,
    duration: '1200s',
};


export default function () {

    const payload = JSON.stringify({
        userId: __VU,
        itemId: Math.floor(Math.random() * 1000),
    });

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const response = http.post(
        'http://localhost:5000/cart/items',
        payload,
        params
    );

    check(response, {
        'status is 200': (r) => r.status === 200,
    });

    sleep(1);
}