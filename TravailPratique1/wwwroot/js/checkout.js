const stripe = Stripe('pk_test_51QuFsZHDXODVYNHYzVETkC0uEBkrZCiWKWtjBW6ve1pVeupzLpfHmQ2RpouZg9sAP2KMDbhJA80GnOWMFNidmdbd009vvw4QmI'

);

const appearance = { /* appearance */ };
const options = {
    layout: {
        type: 'tabs',
        defaultCollapsed: false,
    }
};
const elements = stripe.elements({
    clientSecret
    , appearance
});
const paymentElement = elements.create('payment', options);
paymentElement.mount('#payment-element');