# SmartUI
    SmartUI是是用于构建Wpf程序的一套基础UI框架，内置了丰富的控件样式以及自定义组件，整体风格类似于前端的Element
## 使用手册
  1. 项目依赖项：项目依赖于CalcBinding2.4.0.0版本，目标版本不低于.NET Framework4.7.2
  2. Install-Package Smert 或使用时只需在Visual Suodio中打开“管理Nuget程序包”，搜索并安装“SmartUI”即可使用
## 组件支持
  1. PackIcon 控件是一个强大的Icon图标库，内含10000+多个图标，用户可以自定义图标大小、颜色
    ![image](https://user-images.githubusercontent.com/29591512/110083256-7339d300-7dc9-11eb-9b3c-53d8e070d98a.png)
    ![image](https://user-images.githubusercontent.com/29591512/110083348-8ba9ed80-7dc9-11eb-93cd-eb68f3fdbff7.png)
    ![image](https://user-images.githubusercontent.com/29591512/110083569-d3c91000-7dc9-11eb-98bb-76a409dfd7fb.png)
    使用方式：其中Kind为图标的枚举值，Foreground为图标的颜色
    ```
     <smart:PackIcon Kind="{Binding}" Foreground="Gray"/>
    ```
  3. Button样式
    ![image](https://user-images.githubusercontent.com/29591512/110083013-1ccc9480-7dc9-11eb-8c7d-336ce639d711.png)
    使用方式:如果按钮需要显示图标则需加入附加属性assist:ButtonAssist.Icon="Search"
   
            <UniformGrid Columns="7" Rows="6">
                <Label Content="默认样式"/>
                <Button Content="默认按钮"/>
                <Button Content="主要按钮" Style="{StaticResource PrimaryButtonStyle1}"/>
                <Button Content="成功按钮" assist:ButtonAssist.Icon="Done" Style="{StaticResource SuccessButtonStyle1}"/>
                <Button Content="信息按钮" Style="{StaticResource InfoButtonStyle1}"/>
                <Button Content="警告按钮" Style="{StaticResource WarningButtonStyle1}"/>
                <Button Content="危险按钮" Style="{StaticResource ErrorButtonStyle1}"/>

                <Label Content="默认样式2"/>
                <Button Content="默认按钮" Style="{StaticResource DefaultButtonStyle2}"/>
                <Button Content="主要按钮" Style="{StaticResource PrimaryButtonStyle2}"/>
                <Button Content="成功按钮" Style="{StaticResource SuccessButtonStyle2}"/>
                <Button Content="信息按钮" Style="{StaticResource InfoButtonStyle2}"/>
                <Button Content="警告按钮" Style="{StaticResource WarningButtonStyle2}"/>
                <Button Content="危险按钮" Style="{StaticResource ErrorButtonStyle2}"/>
                
                <Label Content="默认样式3"/>
                <Button Content="默认按钮" Style="{StaticResource FilletDefaultButtonStyle}"/>
                <Button Content="主要按钮" Style="{StaticResource FilletPrimaryButtonStyle}"/>
                <Button Content="成功按钮" Style="{StaticResource FilletSuccessButtonStyle}"/>
                <Button Content="信息按钮" Style="{StaticResource FilletInfoButtonStyle}"/>
                <Button Content="警告按钮" Style="{StaticResource FilletWarningButtonStyle}"/>
                <Button Content="危险按钮" Style="{StaticResource FilletErrorButtonStyle}"/>
                <Label Content="圆形按钮样式"/>
                <Button assist:ButtonAssist.Icon="Search" Style="{StaticResource CircleDefaultButtonStyle}"/>
                <Button assist:ButtonAssist.Icon="Edit" Style="{StaticResource CirclePrimaryButtonStyle}"/>
                <Button assist:ButtonAssist.Icon="Done" Style="{StaticResource CircleSuccessButtonStyle}"/>
                <Button assist:ButtonAssist.Icon="Email" Style="{StaticResource CircleInfoButtonStyle}"/>
                <Button assist:ButtonAssist.Icon="Star" Style="{StaticResource CircleWarningButtonStyle}"/>
                <Button assist:ButtonAssist.Icon="Delete" Style="{StaticResource CircleErrorButtonStyle}"/>
                <Label Content="禁用样式"/>
                <Button Content="默认按钮" IsEnabled="False"/>
                <Button Content="主要按钮" IsEnabled="False" Style="{StaticResource PrimaryButtonStyle1}"/>
                <Button Content="成功按钮" IsEnabled="False" Style="{StaticResource SuccessButtonStyle1}"/>
                <Button Content="信息按钮" IsEnabled="False" Style="{StaticResource InfoButtonStyle1}"/>
                <Button Content="警告按钮" IsEnabled="False" Style="{StaticResource WarningButtonStyle1}"/>
                <Button Content="危险按钮" IsEnabled="False" Style="{StaticResource ErrorButtonStyle1}"/>

                <Label Content="文字按钮样式"/>
                <Button Content="默认按钮" Style="{StaticResource TextButtonStyle}"/>
                <Button Content="禁用按钮" IsEnabled="False" Style="{StaticResource TextButtonStyle}"/>
            </UniformGrid>
   
  3. SmartWindow自定义控件，全新的窗体界面
        使用方式：将Window继承自SmartWindow即可        
  5. TabControl控件样式
  6. Form表单
        1.Radio、
        2.CheckBox、
        3.Input、
        4.Select选择器、
        5.Switch开关
        ![image](https://user-images.githubusercontent.com/29591512/111290646-5468f580-8681-11eb-9d58-4ddcd697f583.png)

        
