// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

contract OrderTracker {
    // ============================================================
    // 1. DEFINE THE ENUM
    // ============================================================
    enum OrderStatus {
        Pending,          // 0 - Default/first value
        Shipped,          // 1
        InTransitPointA,  // 2
        InTransitPointB,  // 3
        Delivered,        // 4
        Cancelled         // 5
    }

    // ============================================================
    // 2. USE THE ENUM
    // ============================================================
    OrderStatus public currentOrderStatus;  // Default: Pending (0)

    // ============================================================
    // 3. FUNCTIONS TO SET AND GET STATUS
    // ============================================================

    // Function to update the order status
    function setStatus(OrderStatus _status) public {
        currentOrderStatus = _status;
    }

    // Function to get current status as a string
    function getStatusString() public view returns (string memory) {
        if (currentOrderStatus == OrderStatus.Pending) return "Pending";
        if (currentOrderStatus == OrderStatus.Shipped) return "Shipped";
        if (currentOrderStatus == OrderStatus.InTransitPointA) return "In Transit Point A";
        if (currentOrderStatus == OrderStatus.InTransitPointB) return "In Transit Point B";
        if (currentOrderStatus == OrderStatus.Delivered) return "Delivered";
        if (currentOrderStatus == OrderStatus.Cancelled) return "Cancelled";
        return "Unknown";
    }

    // Function to get status as a number
    function getStatusNumber() public view returns (uint) {
        return uint(currentOrderStatus);
    }

    // ============================================================
    // 4. PRACTICAL USE: Order System
    // ============================================================

    // Struct for an order
    struct Order {
        uint id;
        string item;
        uint price;
        OrderStatus status;  // Using enum inside struct!
    }

    // Array to store multiple orders
    Order[] public orders;
    uint public orderCount;

    // Create a new order
    function createOrder(string memory _item, uint _price) public {
        orderCount++;
        orders.push(Order({
            id: orderCount,
            item: _item,
            price: _price,
            status: OrderStatus.Pending  // New orders start as Pending
        }));
    }

    // Update order status by ID
    function updateOrderStatus(uint _orderId, OrderStatus _newStatus) public {
        require(_orderId > 0 && _orderId <= orders.length, "Order not found");
        orders[_orderId - 1].status = _newStatus;
    }

    // Get order details
    function getOrder(uint _orderId) public view returns (
        uint id,
        string memory item,
        uint price,
        OrderStatus status
    ) {
        require(_orderId > 0 && _orderId <= orders.length, "Order not found");
        Order memory order = orders[_orderId - 1];
        return (order.id, order.item, order.price, order.status);
    }

    // Get order status as string
    function getOrderStatusString(uint _orderId) public view returns (string memory) {
        require(_orderId > 0 && _orderId <= orders.length, "Order not found");
        OrderStatus status = orders[_orderId - 1].status;

        if (status == OrderStatus.Pending) return "Pending";
        if (status == OrderStatus.Shipped) return "Shipped";
        if (status == OrderStatus.InTransitPointA) return "In Transit Point A";
        if (status == OrderStatus.InTransitPointB) return "In Transit Point B";
        if (status == OrderStatus.Delivered) return "Delivered";
        if (status == OrderStatus.Cancelled) return "Cancelled";
        return "Unknown";
    }
}