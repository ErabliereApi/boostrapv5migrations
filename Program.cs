// This is a program that will migrate a angular code base that use bootstrap 4 to bootstrap 5

// Parse the source folder argument
// Detination is the same as the source folder

var source = args[0];

// Update the package.json file
var packageJson = File.ReadAllText(Path.Combine(source, "..", "package.json"));
packageJson = packageJson.Replace("\"bootstrap\": \"^4.6.1\",", "\"bootstrap\": \"^5.3.2\",");
packageJson = packageJson.Replace("\"popper.js\": \"^1.14.3\",", "\"@popperjs/core\": \"^2.11.8\",");

// remove the line with     "jquery": "^3.7.0",
packageJson = packageJson.Replace("\"jquery\": \"^3.7.0\",", "");

// remove jquery and popper.json from the angular.json file
var angularJson = File.ReadAllText(Path.Combine(source, "..", "angular.json"));
angularJson = angularJson.Replace("\"node_modules/jquery/dist/jquery.slim.min.js\"", "");
angularJson = angularJson.Replace("\"node_modules/popper.js/dist/umd/popper.min.js\"", "");
// save the file
File.WriteAllText(Path.Combine(source, "..", "angular.json"), angularJson);

// Write the file
File.WriteAllText(Path.Combine(source, "..", "package.json"), packageJson);

// Get all html and tsx files in the source folder
var files = Directory.GetFiles(source, "*.*", SearchOption.AllDirectories)
    .Where(s => s.EndsWith(".html") || s.EndsWith(".tsx") || s.EndsWith(".ts"));

// Loop through each file
foreach (var file in files)
{
    // Read the file
    var content = File.ReadAllText(file);

    // Replace the bootstrap 4 classes with bootstrap 5 classes
    // Replace to use the new floating classes
    content = content.Replace("float-right", "float-end");
    content = content.Replace("float-left", "float-start");

    // Replace for modal
    content = content.Replace("data-toggle=\"modal\"", "data-bs-toggle=\"modal\"");
    content = content.Replace("data-target=\"", "data-bs-target=\"");
    content = content.Replace("data-dismiss=", "data-bs-dismiss=");
    content = content.Replace("class=\"close\"", "class=\"btn-close\"");
    content = content.Replace("&times;", "");

    // margin
    content = content.Replace("mr-", "me-");
    content = content.Replace("ml-", "ms-");

    // Write the file
    File.WriteAllText(file, content);
}