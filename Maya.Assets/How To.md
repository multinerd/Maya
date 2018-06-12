This are two ways of doing this. One way is to package the fonts inside the application. The other way is to have the fonts the output folder separately. The difference is mostly the URI you need to load the files. 

### Package with Application

1. Add a `/Fonts` folder to your solution. 
2. Add the True Type Fonts (`*.ttf`) files to that folder 
3. Include the files to the project
4. Select the fonts and add them to the solution
5. Set `BuildAction: Resource` and `Copy To Output Directory: Do not copy`. Your `.csproj` file should now should have a section like this one: 

         <ItemGroup>
          <Resource Include="Fonts\NotoSans-Bold.ttf" />
          <Resource Include="Fonts\NotoSans-BoldItalic.ttf" />
          <Resource Include="Fonts\NotoSans-Italic.ttf" />
          <Resource Include="Fonts\NotoSans-Regular.ttf" />
          <Resource Include="Fonts\NotoSansSymbols-Regular.ttf" />
        </ItemGroup>

7. In `App.xaml` add `<FontFamily>` Resources. It should look like in the following code sample. Note that the URI doesn't contain the filename when packing with the application. 

        <Applicaton ...>
        <Application.Resources>
            <FontFamily x:Key="NotoSans">pack://application:,,,/Fonts/#Noto Sans</FontFamily>
            <FontFamily x:Key="NotoSansSymbols">pack://application:,,,/Fonts/#Noto Sans Symbols</FontFamily>
        </Application.Resources>
        </Application>

8. Apply your Fonts like this: 

        <TextBlock x:Name="myTextBlock" Text="foobar" FontFamily="{StaticResource NotoSans}" 
                   FontSize="10.0" FontStyle="Normal" FontWeight="Regular" />

9. You can also set the font imperatively: 

        myTextBlock.FontFamily = new FontFamily(new Uri("pack://application:,,,/"), "./Fonts/#Noto Sans");

### Copy to Output Directory

1. Add a `/Fonts` folder to your solution. 
2. Add the True Type Fonts (`*.ttf`) files to that order 
3. Include the files to the project
4. Select the fonts and add them to the solution
5. Set `BuildAction: Content` and `Copy To Output Directory: Copy if newer`. Your `.csproj` file should now should have a section like this one: 

         <ItemGroup>
          <Content Include="Fonts\NotoSans-Bold.ttf">
            <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
          </Content>
          <Content Include="Fonts\NotoSans-BoldItalic.ttf">
            <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
          </Content>
          <Content Include="Fonts\NotoSans-Italic.ttf">
            <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
          </Content>
          <Content Include="Fonts\NotoSans-Regular.ttf">
            <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
          </Content>
          <Content Include="Fonts\NotoSansSymbols-Regular.ttf">
            <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
          </Content>
        </ItemGroup>

7. In `App.xaml` add `<FontFamily>` Resources. It should look like in the following code sample. Note that the URI doesn't contain the filename when packing with the application. 

        <Applicaton ...>
        <Application.Resources>
            <FontFamily x:Key="NotoSansRegular">./Fonts/NotoSans-Regular.tts#Noto Sans</FontFamily>
            <FontFamily x:Key="NotoSansItalic">./Fonts/NotoSans-Italic.tts#Noto Sans</FontFamily>
            <FontFamily x:Key="NotoSansBold">./Fonts/NotoSans-Bold.tts#Noto Sans</FontFamily>
            <FontFamily x:Key="NotoSansBoldItalic">./Fonts/NotoSans-BoldItalic.tts#Noto Sans</FontFamily>
            <FontFamily x:Key="NotoSansSymbols">./Fonts/NotoSans-Regular.tts#Noto Sans Symbols</FontFamily>
        </Application.Resources>
        </Application>

8. Apply your Fonts like this: 

        <TextBlock Text="foobar" FontFamily="{StaticResource NotoSansRegular}" 
                   FontSize="10.0" FontStyle="Normal" FontWeight="Regular" />

### References 
* [StackOverflow Source](https://stackoverflow.com/a/39912794/5434784)
* [MSDN: Packaging Fonts with Applications](https://msdn.microsoft.com/en-us/library/ms753303(v=vs.110).aspx)