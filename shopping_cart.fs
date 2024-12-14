// Define the Product type
type Product = {
    Name: string
    Price: float
    Description: string
}

// Product catalog
let productCatalog: Product list = [
    { Name = "Laptop"; Price = 1200.0; Description = "A high-performance laptop" }
    { Name = "Smartphone"; Price = 800.0; Description = "A smartphone with great features" }
    { Name = "Headphones"; Price = 150.0; Description = "Noise-canceling headphones" }
    { Name = "Mouse"; Price = 25.0; Description = "A wireless mouse" }
    { Name = "Keyboard"; Price = 45.0; Description = "A mechanical keyboard" }
]

// Mutable cart
let mutable cart: Product list = []

// Main form
let mainForm = new Form(Text = "Simple Store Simulator", Width = 800, Height = 600)

// Create UI components
let catalogListBox = new ListBox(Width = 350, Height = 300, Left = 10, Top = 10)
let cartListBox = new ListBox(Width = 350, Height = 300, Left = 400, Top = 10)
let addButton = new Button(Text = "Add to Cart", Width = 120, Height = 30, Left = 10, Top = 320)
let removeButton = new Button(Text = "Remove from Cart", Width = 150, Height = 30, Left = 400, Top = 320)
let checkoutButton = new Button(Text = "Checkout", Width = 100, Height = 30, Left = 10, Top = 360)
let totalLabel = new Label(Text = "Total: $0.00", Width = 200, Height = 30, Left = 400, Top = 360)

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
