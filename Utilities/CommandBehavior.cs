using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

public class CommandBehavior : Behavior<UIElement>
{
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(CommandBehavior), new PropertyMetadata(null));

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.MouseDown += AssociatedObject_MouseDown;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseDown -= AssociatedObject_MouseDown;
        base.OnDetaching();
    }

    private void AssociatedObject_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (Command?.CanExecute(null) == true)
        {
            Command.Execute(null);
        }
    }
}
