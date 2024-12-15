open System
open System.Windows.Forms
open System.Drawing

// Define the Product type
type Product = {
    Name: string
    Price: float
    Description: string
}

// Product catalog (use reference to make it "updatable")
let productCatalog = ref [
    { Name = "Laptop"; Price = 1200.0; Description = "A high-performance laptop" }
    { Name = "Smartphone"; Price = 800.0; Description = "A smartphone with great features" }
    { Name = "Headphones"; Price = 150.0; Description = "Noise-canceling headphones" }
    { Name = "Mouse"; Price = 25.0; Description = "A wireless mouse" }
    { Name = "Keyboard"; Price = 45.0; Description = "A mechanical keyboard" }
]

// Use a reference for cart (store a list of (Product, Quantity))
let cart = ref []

// Main form
let mainForm = new Form(Text = "Simple Store Simulator", Width = 800, Height = 600)

// Create UI components
let catalogListBox = new ListBox(Width = 350, Height = 300, Left = 10, Top = 10)
let cartListBox = new ListBox(Width = 350, Height = 300, Left = 400, Top = 10)
let quantityLabel = new Label(Text = "Quantity:", Width = 70, Height = 30, Left = 10, Top = 320) // New label for quantity
let quantityTextBox = new TextBox(Width = 50, Height = 30, Left = 90, Top = 320, Text = "1") // New textbox for quantity
let addButton = new Button(Text = "Add to Cart", Width = 120, Height = 30, Left = 150, Top = 320)
let removeButton = new Button(Text = "Remove from Cart", Width = 150, Height = 30, Left = 400, Top = 320)
let checkoutButton = new Button(Text = "Checkout", Width = 100, Height = 30, Left = 10, Top = 360)
let totalLabel = new Label(Text = "Total: $0.00", Width = 200, Height = 30, Left = 400, Top = 360)
let itemCountLabel = new Label(Text = "Items in Cart: 0", Width = 200, Height = 30, Left = 400, Top = 400) // New label for item count

// Populate the catalog
let updateCatalog () =
    catalogListBox.Items.Clear()
    for product in !productCatalog do
        catalogListBox.Items.Add(sprintf "%s - $%.2f" product.Name product.Price)

// Populate the cart (show product name, price, and quantity)
let updateCart () =
    cartListBox.Items.Clear()
    for (product, quantity) in !cart do
        cartListBox.Items.Add(sprintf "%s - $%.2f (x%d)" product.Name product.Price quantity)

// Populate the catalog
let updateCatalog () =
    catalogListBox.Items.Clear()
    for product in productCatalog do
        catalogListBox.Items.Add(sprintf "%s - $%.2f" product.Name product.Price)

let updateCart () =
    cartListBox.Items.Clear()
    for product in cart do
        cartListBox.Items.Add(sprintf "%s - $%.2f" product.Name product.Price)

// Add a product to the cart
let addToCart () =
    let selectedIndex = catalogListBox.SelectedIndex
    if selectedIndex >= 0 && selectedIndex < productCatalog.Length then
        let selectedProduct = productCatalog.[selectedIndex]
        cart <- selectedProduct :: cart
        updateCart ()
        
// Perform checkout
let checkout () =
    let total = cart |> List.sumBy (fun p -> p.Price)
    MessageBox.Show(sprintf "Total cost: $%.2f\nThank you for shopping!" total, "Checkout") |> ignore
    cart <- []
    updateCart ()
    totalLabel.Text <- "Total: $0.00"

// Remove a product from the cart
let removeFromCart () =
    let selectedIndex = cartListBox.SelectedIndex
    if selectedIndex >= 0 && selectedIndex < (!cart).Length then
        let (product, _) = (!cart).[selectedIndex]
        cart := !cart |> List.filter (fun (p, _) -> p.Name <> product.Name)
        updateCart ()
        updateTotals () // Update total and item count
    else
        MessageBox.Show("Select a product to remove from your cart.") |> ignore

// Perform checkout (reset the cart)
let checkout () =
    let total = !cart |> List.sumBy (fun (p, qty) -> p.Price * float qty)
    let itemCount = !cart |> List.sumBy (fun (_, qty) -> qty)
    MessageBox.Show(sprintf "Total cost: $%.2f\nTotal items: %d\nThank you for shopping!" total itemCount, "Checkout") |> ignore
    cart := [] // Clear the cart
    updateCart ()
    updateTotals () // Reset total and item count

// Wire up event handlers
addButton.Click.Add (fun _ -> 
    addToCart ()
)

removeButton.Click.Add (fun _ -> 
    removeFromCart ()
)

checkoutButton.Click.Add (fun _ -> checkout ())

// Add components to the form
mainForm.Controls.Add(catalogListBox)
mainForm.Controls.Add(cartListBox)
mainForm.Controls.Add(quantityLabel) // Quantity label
mainForm.Controls.Add(quantityTextBox) // Quantity input field
mainForm.Controls.Add(addButton)
mainForm.Controls.Add(removeButton)
mainForm.Controls.Add(checkoutButton)
mainForm.Controls.Add(totalLabel)
mainForm.Controls.Add(itemCountLabel) // Item count label

// Initialize the catalog and run the application
updateCatalog ()
Application.Run(mainForm)
