using Models;

namespace Serial;

public interface IControlConverter
{
    public Control ConvertControl(ActionMap actionMap);
}