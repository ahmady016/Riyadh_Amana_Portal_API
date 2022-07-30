using Microsoft.AspNetCore.Mvc;
using Dtos;

namespace Albums;

[ApiController]
[Route("api/[controller]/[action]")]
public class AlbumsAndPhotosController : ControllerBase
{
    private readonly IAlbumAndPhotoService _service;
    public AlbumsAndPhotosController(IAlbumAndPhotoService service)
    {
        _service = service;
    }


    /// <summary>
    /// listType values (all/deleted/existed)
    /// AlbumsAndPhotos/ListAlbums/all
    /// </summary>
    /// <returns>List of AlbumDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListAlbums([FromRoute] string type)
    {
        return Ok(_service.ListAlbums(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// AlbumsAndPhotos/ListAlbumsPage/all
    /// </summary>
    /// <returns>List of AlbumDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListAlbumsPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListAlbumsPage(type, pageSize ?? 10, pageNumber ?? 1));
    }
    /// <summary>
    /// AlbumsAndPhotos/FindAlbum/[id]
    /// </summary>
    /// <returns>AlbumDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindAlbum(Guid id)
    {
        return Ok(_service.FindOneAlbum(id));
    }
    /// <summary>
    /// AlbumsAndPhotos/FindAlbums/[id, id, id]
    /// </summary>
    /// <returns>List Of AlbumDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindAlbums(string ids)
    {
        return Ok(_service.FindManyAlbums(ids));
    }

    /// <summary>
    /// AlbumsAndPhotos/AddAlbum
    /// </summary>
    /// <returns>AlbumDto</returns>
    [HttpPost]
    public virtual IActionResult AddAlbum([FromBody] CreateAlbumInput input)
    {
        return Ok(_service.AddAlbum(input));
    }
    /// <summary>
    /// AlbumsAndPhotos/AddAlbumWithPhotos
    /// </summary>
    /// <returns>AlbumDto</returns>
    [HttpPost]
    public virtual IActionResult AddAlbumWithPhotos([FromBody] CreateAlbumWithPhotosInput input)
    {
        return Ok(_service.AddAlbumWithPhotos(input));
    }
    /// <summary>
    /// AlbumsAndPhotos/AddManyAlbums
    /// </summary>
    /// <returns>List Of AlbumDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyAlbums([FromBody] List<CreateAlbumInput> inputs)
    {
        return Ok(_service.AddManyAlbums(inputs));
    }

    /// <summary>
    /// AlbumsAndPhotos/UpdateAlbum
    /// </summary>
    /// <returns>AlbumDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateAlbum([FromBody] UpdateAlbumInput input)
    {
        return Ok(_service.UpdateAlbum(input));
    }
    /// <summary>
    /// AlbumsAndPhotos/UpdateManyAlbums
    /// </summary>
    /// <returns>List Of AlbumDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyAlbums([FromBody] List<UpdateAlbumInput> inputs)
    {
        return Ok(_service.UpdateManyAlbums(inputs));
    }

    /// <summary>
    /// AlbumsAndPhotos/DeleteAlbum
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeleteAlbum(Guid id)
    {
        return Ok(_service.DeleteAlbum(id));
    }
    /// <summary>
    /// AlbumsAndPhotos/ActivateAlbum
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivateAlbum(Guid id)
    {
        return Ok(_service.ActivateAlbum(id));
    }


    /// <summary>
    /// listType values (all/deleted/existed)
    /// AlbumsAndPhotos/ListPhotos/all
    /// </summary>
    /// <returns>List of PhotoDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListPhotos([FromRoute] string type)
    {
        return Ok(_service.ListPhotos(type));
    }
    /// <summary>
    /// listType values (all/deleted/existed)
    /// AlbumsAndPhotos/ListPhotosPage/all
    /// </summary>
    /// <returns>List of PhotoDto</returns>
    [HttpGet("{type}")]
    public virtual IActionResult ListPhotosPage(
        [FromRoute] string type,
        [FromQuery] int? pageSize,
        [FromQuery] int? pageNumber
    )
    {
        return Ok(_service.ListPhotosPage(type, pageSize ?? 10, pageNumber ?? 1));
    }
    /// <summary>
    /// AlbumsAndPhotos/FindPhoto/[id]
    /// </summary>
    /// <returns>PhotoDto</returns>
    [HttpGet("{id}")]
    public IActionResult FindPhoto(Guid id)
    {
        return Ok(_service.FindOnePhoto(id));
    }
    /// <summary>
    /// AlbumsAndPhotos/FindPhotos/[id, id, id]
    /// </summary>
    /// <returns>List Of PhotoDto</returns>
    [HttpGet("{ids}")]
    public IActionResult FindPhotos(string ids)
    {
        return Ok(_service.FindManyPhotos(ids));
    }

    /// <summary>
    /// AlbumsAndPhotos/AddPhoto
    /// </summary>
    /// <returns>PhotoDto</returns>
    [HttpPost]
    public virtual IActionResult AddPhoto([FromBody] CreatePhotoInput input)
    {
        return Ok(_service.AddPhoto(input));
    }
    /// <summary>
    /// AlbumsAndPhotos/AddManyPhotos
    /// </summary>
    /// <returns>List Of PhotoDto</returns>
    [HttpPost]
    public virtual IActionResult AddManyPhotos([FromBody] List<CreatePhotoInput> inputs)
    {
        return Ok(_service.AddManyPhotos(inputs));
    }

    /// <summary>
    /// AlbumsAndPhotos/UpdatePhoto
    /// </summary>
    /// <returns>PhotoDto</returns>
    [HttpPut]
    public virtual IActionResult UpdatePhoto([FromBody] UpdatePhotoInput input)
    {
        return Ok(_service.UpdatePhoto(input));
    }
    /// <summary>
    /// AlbumsAndPhotos/UpdateManyPhotos
    /// </summary>
    /// <returns>List Of PhotoDto</returns>
    [HttpPut]
    public virtual IActionResult UpdateManyPhotos([FromBody] List<UpdatePhotoInput> inputs)
    {
        return Ok(_service.UpdateManyPhotos(inputs));
    }

    /// <summary>
    /// AlbumsAndPhotos/DeletePhoto
    /// </summary>
    /// <returns>bool</returns>
    [HttpDelete]
    public virtual IActionResult DeletePhoto(Guid id)
    {
        return Ok(_service.DeletePhoto(id));
    }
    /// <summary>
    /// AlbumsAndPhotos/ActivatePhoto
    /// </summary>
    /// <returns>bool</returns>
    [HttpPut]
    public virtual IActionResult ActivatePhoto(Guid id)
    {
        return Ok(_service.ActivatePhoto(id));
    }

}
