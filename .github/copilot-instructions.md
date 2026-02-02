# Copilot Instructions for HaloPixelToolBox

## Project Overview

HaloPixelToolBox (花再音响工具箱) is a desktop utility toolbox application designed to provide rich and intuitive information display capabilities for audio/desktop devices. The project is primarily focused on displaying lyrics from various music platforms, starting with NetEase Cloud Music support.

## Technology Stack

- **Framework**: .NET 8 with WinUI 3
- **Platform**: Windows 10.0.22621.0 (minimum version: 10.0.17763.0)
- **UI Framework**: Windows App SDK 1.7.x with WinUI 3
- **Architecture**: MVVM pattern using CommunityToolkit.Mvvm
- **Dependencies**:
  - XFEExtension.NetCore libraries (various utilities)
  - CommunityToolkit.Mvvm for MVVM implementation
  - CommunityToolkit.WinUI.Controls for UI controls
  - Microsoft.WindowsAppSDK for Windows App development

## Project Structure

```
HaloPixelToolBox/
├── HaloPixelToolBox/              # Main WinUI application
│   ├── Views/                     # XAML views and code-behind
│   ├── ViewModels/                # View models
│   ├── Interface/Services/        # Service interfaces
│   ├── Implements/Services/       # Service implementations
│   └── Assets/                    # Application assets and resources
├── HaloPixelToolBox.Core/         # Core business logic
│   ├── Models/                    # Data models
│   └── Utilities/                 # Helper utilities
├── HaloPixelToolBox.Test/         # Test project (XUnit)
├── HaloPixelToolBox.Installer/    # Installer project
└── HaloPixelToolBox.Installer.Package/ # Installer packaging
```

## Build and Test

### Building the Project
```bash
# Build the entire solution
dotnet build HaloPixelToolBox.slnx

# Build for specific platform
dotnet build HaloPixelToolBox.slnx -p:Platform=x64
```

### Running Tests
```bash
# Run all tests
dotnet test HaloPixelToolBox/HaloPixelToolBox.Test/HaloPixelToolBox.Test.csproj
```

### Platform Support
The application targets three platforms:
- x86 (win-x86)
- x64 (win-x64)
- ARM64 (win-arm64)

## Coding Standards and Conventions

### C# Style Guidelines
1. **Naming Conventions**:
   - Use PascalCase for class names, method names, and properties
   - Use camelCase for private fields and local variables
   - Use descriptive names in English for code elements
   
2. **MVVM Pattern**:
   - ViewModels should inherit from `ViewModelBase`
   - Use `[ObservableProperty]` attribute from CommunityToolkit.Mvvm for bindable properties
   - Use `[RelayCommand]` attribute for command methods
   - Services should be registered with `ServiceManager`

3. **Code Organization**:
   - Keep views in `Views/` folder with corresponding `.xaml.cs` files
   - Keep view models in `ViewModels/` folder
   - Service interfaces go in `Interface/Services/`
   - Service implementations go in `Implements/Services/`

4. **Nullable Reference Types**:
   - The project has nullable reference types enabled
   - Use `?` suffix for nullable types
   - Handle null cases appropriately

5. **Using Directives**:
   - Global usings are configured in the project for common namespaces
   - Include necessary usings at the top of each file

### XAML Guidelines
1. Use proper indentation and formatting
2. Follow WinUI 3 control naming conventions
3. Bind to ViewModel properties using `{x:Bind}` where possible

## Language and Documentation

### Bilingual Support
The project serves a **bilingual community** (Chinese and English):
- **Code**: Write code, comments, and technical documentation in **English**
- **User-Facing Content**: Issues, README, and user documentation support **Chinese**
- **Issue Translation**: The repository has automatic translation configured for issues

### Comments
- Add comments for complex logic or non-obvious code
- Comments can be in English
- User-facing strings should support localization where applicable

## Key Features and Development Focus

### Current Features
- ✅ NetEase Cloud Music lyrics display

### Planned Features
- 🚀 Additional music platform lyrics support
- 💬 Social media message notifications
- 🎨 More customization options

## Working with the Repository

### Issue and PR Guidelines
- Issues can be submitted in Chinese (automatic translation is configured)
- Read `CONTRIBUTING.md` for detailed contribution guidelines
- Use the issue templates provided in `.github/ISSUE_TEMPLATE/`
- PRs should:
  - Have clear descriptions in English
  - Follow existing code style
  - Not break existing functionality
  - Include tests if adding new features

### Git Workflow
- Feature branches should be created from main
- Commit messages should be clear and descriptive
- Follow conventional commit format when possible

## Service Architecture

The application uses a service-based architecture:
- Services are defined as interfaces in `Interface/Services/`
- Implementations are in `Implements/Services/`
- Services are accessed via `ServiceManager` (from XFEExtension.NetCore)
- Common services:
  - `INavigationService`: Page navigation
  - `IMessageService`: User notifications
  - `ITrayIconService`: System tray management
  - `IUpgradeService`: Application updates
  - `IPageService`: Page lifecycle management

## Best Practices

1. **Use Existing Libraries**: Leverage XFEExtension.NetCore utilities where possible
2. **Follow MVVM**: Keep business logic in ViewModels, not in code-behind
3. **Service Pattern**: Register new services with ServiceManager
4. **Error Handling**: Handle exceptions gracefully and provide user feedback
5. **Resource Management**: Properly dispose of resources and event handlers
6. **UI Threading**: Use proper threading for UI updates in WinUI 3
7. **Async/Await**: Use async patterns for I/O operations

## Testing

- Test project uses XFEExtension.NetCore.XUnit
- Write unit tests for business logic in Core project
- Integration tests for services and ViewModels
- Keep tests focused and maintainable

## Special Considerations

### Windows App SDK Specifics
- The app uses self-contained Windows App SDK deployment
- MSIX packaging is enabled but WindowsPackageType is set to None
- Application uses unsafe blocks for certain operations

### Installer Project
- Separate installer and updater project included
- Installer embeds the application as `Source.zip`
- See `HaloPixelToolBox.Installer/README.md` for setup instructions

## Security

- The project handles user data and music platform integration
- Be cautious with API credentials and user privacy
- Follow secure coding practices for network operations
- Validate all user inputs

## Additional Resources

- Main README: `/README.md`
- Contributing Guidelines: `/CONTRIBUTING.md`
- Installer Documentation: `/HaloPixelToolBox.Installer/README.md`
