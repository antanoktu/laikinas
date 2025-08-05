# 🌱 Advanced Plant & Tree Identifier

A sophisticated plant identification application that uses multiple authentication methods and comprehensive plant feature analysis to achieve high-confidence plant identification.

## 🚀 Features

### Multiple Authentication Methods
The system implements 5 different authentication approaches to ensure accurate plant identification:

1. **Basic Plant Identification** - Standard API-based identification
2. **Detailed Feature Analysis** - Comprehensive plant characteristics analysis
3. **Morphological Analysis** - Structural and form-based identification
4. **Ecological Context Analysis** - Environment and habitat-based identification
5. **Seasonal and Phenological Analysis** - Time-based and lifecycle identification

### Confidence-Based Loop System
- **95% Confidence Threshold**: The system continues authentication attempts until reaching 95% confidence
- **Maximum 5 Attempts**: Prevents infinite loops while ensuring thorough analysis
- **Weighted Confidence Calculation**: Different authentication methods have different reliability weights

### Comprehensive Plant Feature Analysis

The system analyzes plants using 6 major categories of characteristics:

#### 1. Bendrieji požymiai (visoms augalų grupėms)
- **Ūkis/Gyvavimo forma**: žoliniai augalai, puskardžiai, lianos, sukulentai, kerinukai
- **Šaknų sistema**: šakniastiebis, šaknies rozetė, skritulė, šakniastiebis, storas šakniagumbis
- **Stiebo sandara**: Structure and composition
- **Stiebo skerspjūvio forma**: apvalus, kvadratinis, trikampis
- **Paviršiaus tekstūra**: štai, pūkuotas/vilnietas, lysvas, glotnus
- **Vystymosi tipas**: kilpojančios lianos ar stačios šakos
- **Plaušinių struktūros**: šiaudai, pūkai, blakstienėlės, žvynai ant stiebo

#### 2. Lapų ir pumpurų požymiai
- **Lapo išsidėstymas ant stiebo**: priešingas, išsiskleidęs (alternate), vainikinis (whorled)
- **Lapo tipas**: paprastas, sudėtinis (kelios skiltelės), karpyta, plunksniška
- **Lapo kraštas (marginalia)**: lygus, dantytas, banguotas, dvigubai dantytas, skylėtas
- **Vyvavimas**: plaušinis (pamatiniai plaukeliai), vaškinis apvalkalas, žvyneliai
- **Venacija**: lygiagreti (kaip varpiniai), tinklinė, giliaspalvė
- **Lapo pagrindo ir viršūnės forma**: omištas, ežero formos, trumpas kotelis, ilgas kotelis, be kotelio (sessile)
- **Pumpuro tipas ir padėtis**: lapinis, žiedinis, sumedėjęs, užuomazginis

#### 3. Šaknų ir kamieno požymiai (ypač medžiams ir krūmams)
- **Kamieno žievės tekstūra**: minkšta, šerpetojanti, raukšlėta, skalautinė, šakinė
- **Kamieno krypčių santykis**: stiebas/kamienas (medžių santykinis aukštis ir skersmuo)
- **Šakų išsidėstymas**: šakos kampas nuo pagrindo, plunksniškas ar kuokštinis išsidėstymas
- **Šaknų tipo matmenys**: gilesnės šaknys, šaknų smaigalys, trapūs šaknų ūgliai

#### 4. Žiedų ir žydėjimo ypatybės
- **Inflorescencijos tipas**: kekė, šluotelė, žiedynas, galvutė, skėtinė kekė
- **Žiedo spalva ir simetrija**: radialinė, šoninė, dvikamienė; dvimatė, tridimensiškai išsikišusi
- **Žiedo statmenys dydžiai**: vyriška ir moteriška dalis, gaubteliai, vainiklapiai, kuokeliai
- **Kvapas**: malonus, aitrus, nematomas (svarbi atpažinimui)
- **Žydėjimo metas**: pavasaris, vasara, ruduo; tik vienos dienos žiedas ar ilgai žydinti rūšis

#### 5. Vaisių ir sėklų požymiai
- **Vaisiaus tipas**: uoga, kaulavaisis, sėklelis, ankštara, riešutas
- **Vaisiaus spalva, forma, raštai**: dryžuotas, taškuotas, su spygliukais, blizgus, matiniai
- **Vaisiaus sukabos mechanizmas**: dešimtseklis atskiriamas, neskilinėja, atsidaro dviejose sluoksnėse
- **Sėklos paviršius**: guotinis, plunksnelėmis paženklintas, rugotas, lygus

#### 6. Fenologija ir ekologija
- **Fenologiniai požymiai**: lapų išsiskleidimas, žydėjimo, derėjimo laikas
- **Augimo aplinka**: drėgna pieva, sausa palaukė, miško paklotė, paviršiaus skardis
- **Geografinis paplitimas**: endeminės, invazinės, plačiai paplitusios
- **Rūšies prisitaikymai**: toleruoja drėgmę/aridumą, apšvietimo poreikiai

## 🛠️ Technical Implementation

### Architecture
- **Blazor WebAssembly**: Modern web application framework
- **Dependency Injection**: Proper service registration and management
- **Async/Await Pattern**: Non-blocking operations for better performance
- **Component-Based UI**: Reusable and maintainable user interface

### Key Components

#### PlantIdService
- Implements multiple authentication methods
- Confidence-based loop system
- Weighted confidence calculation
- Error handling and fallback mechanisms

#### PlantFeatureAnalyzer
- Comprehensive plant feature analysis
- 30+ different plant characteristics
- Feature confidence calculation
- Modular analysis methods

#### AuthenticationSummary Component
- Displays comprehensive authentication results
- Shows all used authentication methods
- Detailed plant characteristics breakdown
- Visual confidence indicators

### Confidence Calculation
```csharp
// Weighted average based on method reliability
double weight = 1.0;
if (method.Contains("Detailed")) weight = 1.5;
if (method.Contains("Morphological")) weight = 1.3;
if (method.Contains("Ecological")) weight = 1.2;
if (method.Contains("Seasonal")) weight = 1.1;
```

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- Modern web browser
- Plant.id API key (optional for demo mode)

### Installation
1. Clone the repository
2. Navigate to the project directory
3. Update the API key in `PlantIdService.cs` (line 12)
4. Run the application:
   ```bash
   dotnet run
   ```

### Configuration
- Set your Plant.id API key in `PlantIdService.cs`
- Adjust confidence threshold in `PlantIdService.cs` (line 13)
- Modify maximum attempts in the identification loop (line 20)

## 📊 Usage

1. **Upload Image**: Drag and drop or click to upload a plant image
2. **Start Identification**: Click "Start Advanced Identification"
3. **Monitor Progress**: Watch the authentication attempts in real-time
4. **View Results**: See comprehensive plant analysis and authentication summary

## 🔧 Customization

### Adding New Authentication Methods
1. Create a new method in `PlantIdService`
2. Add it to the identification loop
3. Update confidence calculation weights
4. Add to the UI display

### Modifying Plant Features
1. Update `PlantFeatures` model in `Models/PlantIdentificationResult.cs`
2. Add corresponding analysis methods in `PlantFeatureAnalyzer`
3. Update the UI components to display new features

## 🎯 Benefits

- **High Accuracy**: Multiple authentication methods ensure reliable identification
- **Comprehensive Analysis**: 30+ plant characteristics analyzed
- **Confidence-Based**: Only returns results with 95%+ confidence
- **User-Friendly**: Clear visual feedback and detailed results
- **Extensible**: Easy to add new authentication methods and features

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## 📞 Support

For questions or support, please open an issue in the repository. 