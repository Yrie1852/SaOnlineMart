document.addEventListener('DOMContentLoaded', () => {
    if (document.getElementById('products')) {
        fetchProducts();
    }

    if (document.getElementById('product-details')) {
        const productId = new URLSearchParams(window.location.search).get('id');
        fetchProductDetails(productId);
    }

    if (document.getElementById('cart-items')) {
        fetchCartItems();
    }

    if (document.getElementById('checkout-form')) {
        setupCheckoutForm();
    }
});

function fetchProducts() {
    fetch('https://localhost:5001/api/products')
        .then(response => response.json())
        .then(products => {
            const productContainer = document.getElementById('products');
            productContainer.innerHTML = products.map(product => `
                <div class="product-item">
                    <h2>${product.name}</h2>
                    <p>${product.description}</p>
                    <p>$${product.price}</p>
                    <a href="product.html?id=${product.id}">View Details</a>
                </div>
            `).join('');
        });
}

function fetchProductDetails(productId) {
    fetch(`https://localhost:5001/api/products/${productId}`)
        .then(response => response.json())
        .then(product => {
            const productDetailsContainer = document.getElementById('product-details');
            productDetailsContainer.innerHTML = `
                <h2>${product.name}</h2>
                <p>${product.description}</p>
                <p>$${product.price}</p>
                <img src="${product.imageUrl}" alt="${product.name}">
                <button onclick="addToCart(${product.id})">Add to Cart</button>
            `;
        });
}

function fetchCartItems() {
    
}   

function setupCheckoutForm() {
    
}

function addToCart(productId) {
  
}
gi