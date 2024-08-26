const API_URL = 'https://localhost:7153/api';
async function createOrder() {
    const orderDetails = [];
    document.querySelectorAll('.order-item').forEach(item => {
        const productName = item.querySelector('.productName').value;
        const quantity = parseInt(item.querySelector('.quantity').value);
        const price = parseFloat(item.querySelector('.price').value);
        orderDetails.push({ productName, quantity, price });
    });

    try {
        const response = await fetch(`${API_URL}/order/create`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ orderDetails })
        });

        const result = await response.json();

        if (response.ok) {
            alert('Order created successfully!');
            addOrdersToTable(orderDetails);
            document.getElementById('orderForm').reset(); 
        } else {
            const errorMessage = result.title || 'Error creating order';
            alert(`Error: ${errorMessage}`);
        }
    } catch (error) {
        alert('Network error: ' + error.message);
    }
}

function addOrderItem() {
    const orderDetailsDiv = document.getElementById('orderDetails');
    const orderItemDiv = document.createElement('div');
    orderItemDiv.className = 'order-item';
    orderItemDiv.innerHTML = `
        <input type="text" placeholder="Product Name" class="productName" required>
        <input type="number" placeholder="Quantity" class="quantity" required>
        <input type="number" placeholder="Price" class="price" required>
    `;
    orderDetailsDiv.appendChild(orderItemDiv);
}

function addOrdersToTable(orders) {
    const ordersTableBody = document.getElementById('ordersTable').getElementsByTagName('tbody')[0];
    orders.forEach(order => {
        const row = document.createElement('tr');
        row.innerHTML = `
            <td>${order.productName}</td>
            <td>${order.quantity}</td>
            <td>${order.price}</td>
            <td>
                <button onclick="showDeleteOptions(this)">...</button>
                <div class="delete-options" style="display:none;">
                    <button onclick="deleteOrder(this)">Delete</button>
                </div>
            </td>
        `;
        ordersTableBody.appendChild(row);
    });
}

function showDeleteOptions(button) {
    const deleteOptions = button.nextElementSibling;
    deleteOptions.style.display = deleteOptions.style.display === 'none' ? 'block' : 'none';
}

function deleteOrder(button) {
    const row = button.closest('tr');
    row.remove();
    alert('Order deleted successfully!');
}
