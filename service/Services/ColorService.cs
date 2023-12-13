using infrastructure.Entities;
using infrastructure.Entities.Helper;
using infrastructure.Repositories;
using infrastructure.Repositories.Factory;
using infrastructure.Repositories.Interface;
using Color = System.Drawing.Color;

namespace service.Services;

public class ColorService
{
    private readonly ICrud<ColorType> _colorRepository;
    private readonly IColorMapper _colorMapper;
    
    public ColorService(CRUDFactory crudFactory, IColorMapper colorMapper)
    {
        _colorRepository = crudFactory.GetRepository<ColorType>(RepoType.ColorTypeRepo);
        _colorMapper = colorMapper;
    }
    
    public ColorType GetColorById(int colorid)
    {
        return _colorRepository.Read(colorid);
    }

    public void CreateColor(ColorType colorType)
    {
        _colorRepository.Create(colorType);
    }

    public void DeleteColor(int colorId)
    {
        _colorRepository.Delete(colorId);
    }

    // This method is from the ColorMapperRepository and it adds selected colors to the selected product
    public void AddColorToProduct(int productId, List<int> colorIds)
    {
        _colorMapper.AddColorToProduct(productId, colorIds);
    }
    
    // This Method is from the ColorMapperRepository and it removes selected colors from the selected product
    public void DeleteColorsFromProduct(int productId, List<int> colorIds)
    {
        _colorMapper.RemoveColorFromProduct(productId, colorIds);
    }

    public List<ColorType> GetColorsByProductId(int productId)
    {
      return _colorMapper.GetColorsByProductId(productId);
    }

    //This method is from the ColorMapperRepository and it gets all colors in the colortype table
    public List<ColorType> GetAllColorTypes()
    {
        return _colorMapper.GetAllColorTypes();
    }


    
}