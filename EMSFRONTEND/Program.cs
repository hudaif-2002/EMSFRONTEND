/*using EMSFRONTEND.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Register EmpTaskListService with HttpClient and BaseAddress
builder.Services.AddHttpClient<EmpTaskListService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5293"); // Adjust to your API Base URL
});

// Register LoginSignupService similarly
builder.Services.AddHttpClient<LoginSignupService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5293"); // Adjust to your backend URL
});

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Session timeout of 30 minutes
    options.Cookie.HttpOnly = true;  // Ensure cookies are not accessible via JavaScript
    options.Cookie.IsEssential = true;  // Make sure the cookie is essential
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Enable session management

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"); // Adjust the default route if needed

app.Run();*/







using EMSFRONTEND.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Register EmpTaskListService with HttpClient and BaseAddress
builder.Services.AddHttpClient<EmpTaskListService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5293"); // Adjust to your API Base URL
});

// Register LoginSignupService similarly
builder.Services.AddHttpClient<LoginSignupService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5293"); // Adjust to your backend URL
});

builder.Services.AddHttpClient<PerformanceService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5293"); // Adjust to your backend URL
});

// Add session support
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // Session timeout of 30 minutes
    options.Cookie.HttpOnly = true;  // Ensure cookies are not accessible via JavaScript
    options.Cookie.IsEssential = true;  // Make sure the cookie is essential
});

// Register IHttpContextAccessor for session access in services
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // Enable session management

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"); // Adjust the default route if needed

app.Run();
