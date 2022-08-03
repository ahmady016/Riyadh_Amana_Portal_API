﻿using System.Linq;
using System.Net;
using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;
using Dtos;

namespace Navs;

public class NavAndNavLinkService : INavAndNavLinkService
{
    private readonly ICRUDService _crudService;
    private readonly IMapper _mapper;
    private readonly ILogger<Nav> _logger;
    private string _errorMessage;

    public NavAndNavLinkService(
        ICRUDService curdService,
        IMapper mapper,
        ILogger<Nav> logger
    )
    {
        _crudService = curdService;
        _mapper = mapper;
        _logger = logger;
    }

    #region private Methods
    private Nav GetNavById(Guid id)
    {
        var Nav = _crudService.Find<Nav, Guid>(id);
        if (Nav is null)
        {
            _errorMessage = $"Nav Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return Nav;
    }
    private List<Nav> GetNavsByIds(List<Guid> ids)
    {
        var Navs = _crudService.GetList<Nav, Guid>(e => ids.Contains(e.Id));
        if (Navs.Count == 0)
        {
            _errorMessage = $"No Any Nav Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return Navs;
    }
    private static void FillRestPropsWithOldValues(Nav oldItem, Nav newItem)
    {
        newItem.CreatedAt = oldItem.CreatedAt;
        newItem.CreatedBy = oldItem.CreatedBy;
        newItem.UpdatedAt = oldItem.UpdatedAt;
        newItem.UpdatedBy = oldItem.UpdatedBy;
        newItem.IsActive = oldItem.IsActive;
        newItem.ActivatedAt = oldItem.ActivatedAt;
        newItem.ActivatedBy = oldItem.ActivatedBy;
        newItem.IsDeleted = oldItem.IsDeleted;
        newItem.DeletedAt = oldItem.DeletedAt;
        newItem.DeletedBy = oldItem.DeletedBy;
    }

    private NavLink GetNavLinkById(Guid id)
    {
        var NavLink = _crudService.Find<NavLink, Guid>(id);
        if (NavLink is null)
        {
            _errorMessage = $"NavLink Record with Id: {id} Not Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return NavLink;
    }
    private List<NavLink> GetNavLinksByIds(List<Guid> ids)
    {
        var NavLinks = _crudService.GetList<NavLink, Guid>(e => ids.Contains(e.Id));
        if (NavLinks.Count == 0)
        {
            _errorMessage = $"No Any NavLink Records Found";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.NotFound);
        }
        return NavLinks;
    }
    private static void FillRestPropsWithOldValues(NavLink oldItem, NavLink newItem)
    {
        newItem.CreatedAt = oldItem.CreatedAt;
        newItem.CreatedBy = oldItem.CreatedBy;
        newItem.UpdatedAt = oldItem.UpdatedAt;
        newItem.UpdatedBy = oldItem.UpdatedBy;
        newItem.IsActive = oldItem.IsActive;
        newItem.ActivatedAt = oldItem.ActivatedAt;
        newItem.ActivatedBy = oldItem.ActivatedBy;
        newItem.IsDeleted = oldItem.IsDeleted;
        newItem.DeletedAt = oldItem.DeletedAt;
        newItem.DeletedBy = oldItem.DeletedBy;
    }
    #endregion

    public List<NavDto> ListNavs(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<Nav, Guid>(),
            "deleted" => _crudService.GetList<Nav, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<Nav, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<NavDto>>(list);
    }
    public PageResult<NavDto> ListNavsPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<Nav>(),
            "deleted" => _crudService.GetQuery<Nav>(e => e.IsDeleted),
            _ => _crudService.GetQuery<Nav>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<NavDto>()
        {
            PageItems = _mapper.Map<List<NavDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }
    public NavDto FindOneNav(Guid id)
    {
        var Nav = GetNavById(id);
        return _mapper.Map<NavDto>(Nav);
    }
    public List<NavDto> FindManyNavs(string ids)
    {
        if (String.IsNullOrEmpty(ids) || String.IsNullOrWhiteSpace(ids))
        {
            _errorMessage = $"Nav: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetNavsByIds(_ids);
        return _mapper.Map<List<NavDto>>(list);
    }

    public NavDto AddNav(CreateNavInput input)
    {
        var oldNav = _crudService.GetOne<Nav>(e=> e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
        if (oldNav is not null)
        {
            _errorMessage = $"Nav: TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        var Nav = _mapper.Map<Nav>(input);
        var createdNav = _crudService.Add<Nav, Guid>(Nav);
        _crudService.SaveChanges();
        return _mapper.Map<NavDto>(createdNav);
    }
    public NavDto AddNavWithLinks(CreateNavWithLinksInput input)
    {
        var oldNav = _crudService.GetOne<Nav>(e => e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
        if (oldNav is not null)
        {
            _errorMessage = $"Nav: TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        var Nav = _mapper.Map<Nav>(input);
        var createdNav = _crudService.Add<Nav, Guid>(Nav);
        _crudService.SaveChanges();
        return _mapper.Map<NavDto>(createdNav);
    }
    public List<NavDto> AddManyNavs(List<CreateNavInput> inputs)
    {
        var titlesArList = inputs.Select(e=>e.TitleAr).ToList();
        var titlesEnList = inputs.Select(e=>e.TitleEn).ToList();

        var NavsExisted = _crudService.GetList<Nav,Guid>(e=> titlesArList.Contains(e.TitleAr) || titlesEnList.Contains(e.TitleEn));
        if (NavsExisted.Count != 0)
        {
            _errorMessage = $"Nav: Navs List Is rejected , Some TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        var Navs = _mapper.Map<List<Nav>>(inputs);
        var createdNavs = _crudService.AddAndGetRange<Nav, Guid>(Navs);
        _crudService.SaveChanges();
        return _mapper.Map<List<NavDto>>(createdNavs);
    }

    public NavDto UpdateNav(UpdateNavInput input)
    {
        var oldNav = GetNavById(input.Id);
        if (oldNav.TitleAr != input.TitleAr || oldNav.TitleEn != input.TitleEn ) {
            var NavExisted = _crudService.GetOne<Nav>(e => e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
            if (NavExisted is not null) {
                _errorMessage = $"Nav: TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        var newNav = _mapper.Map<Nav>(input);
        FillRestPropsWithOldValues(oldNav, newNav);
        var updatedNav = _crudService.Update<Nav, Guid>(newNav);
        _crudService.SaveChanges();

        return _mapper.Map<NavDto>(updatedNav);
    }
    public List<NavDto> UpdateManyNavs(List<UpdateNavInput> inputs)
    {
        var oldNavs = GetNavsByIds(inputs.Select(x => x.Id).ToList());

        var titlesArList = inputs.Select(e => e.TitleAr).ToList();
        var titlesEnList = inputs.Select(e => e.TitleEn).ToList();

        var oldNavsTitlesAr = oldNavs
            .Where(m => !titlesArList.Contains(m.TitleAr))
            .Select(e => e.TitleAr)
            .ToList();
        var oldNavsTitleEn = oldNavs
            .Where(m => !titlesEnList.Contains(m.TitleEn))
            .Select(e => e.TitleEn)
            .ToList();

        if (oldNavsTitlesAr.Count != 0 || oldNavsTitleEn.Count != 0)
        {
            var NavsExisted = _crudService.GetList<NavLink, Guid>(e => oldNavsTitlesAr.Contains(e.TitleAr) || oldNavsTitleEn.Contains(e.TitleEn));
            if (NavsExisted.Count != 0)
            {
                _errorMessage = $"Nav: Nav List Is rejected , Some TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        var newNavs = _mapper.Map<List<Nav>>(inputs);

        for (int i = 0; i < oldNavs.Count; i++)
            FillRestPropsWithOldValues(oldNavs[i], newNavs[i]);
        var updatedNavs = _crudService.UpdateAndGetRange<Nav, Guid>(newNavs);
        _crudService.SaveChanges();

        return _mapper.Map<List<NavDto>>(updatedNavs);
    }

    public bool DeleteNav(Guid id)
    {
        var Nav = GetNavById(id);
        _crudService.SoftDelete<Nav, Guid>(Nav);
        _crudService.SaveChanges();
        return true;
    }
    public bool ActivateNav(Guid id)
    {
        var Nav = GetNavById(id);
        _crudService.Activate<Nav, Guid>(Nav);
        _crudService.SaveChanges();
        return true;
    }

    public List<NavLinkDto> ListNavLinks(string type)
    {
        var list = type.ToLower() switch
        {
            "all" => _crudService.GetAll<NavLink, Guid>(),
            "deleted" => _crudService.GetList<NavLink, Guid>(e => e.IsDeleted),
            _ => _crudService.GetList<NavLink, Guid>(e => !e.IsDeleted),
        };
        return _mapper.Map<List<NavLinkDto>>(list);
    }
    public PageResult<NavLinkDto> ListNavLinksPage(string type, int pageSize, int pageNumber)
    {
        var query = type.ToLower() switch
        {
            "all" => _crudService.GetQuery<NavLink>(),
            "deleted" => _crudService.GetQuery<NavLink>(e => e.IsDeleted),
            _ => _crudService.GetQuery<NavLink>(e => !e.IsDeleted),
        };
        var page = _crudService.GetPage(query, pageSize, pageNumber);
        return new PageResult<NavLinkDto>()
        {
            PageItems = _mapper.Map<List<NavLinkDto>>(page.PageItems),
            TotalItems = page.TotalItems,
            TotalPages = page.TotalPages
        };
    }
    public NavLinkDto FindOneNavLink(Guid id)
    {
        var NavLink = GetNavLinkById(id);
        return _mapper.Map<NavLinkDto>(NavLink);
    }
    public List<NavLinkDto> FindManyNavLinks(string ids)
    {
        if (String.IsNullOrEmpty(ids) || String.IsNullOrWhiteSpace(ids))
        {
            _errorMessage = $"NavLink: Must supply comma separated string of ids";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var _ids = ids.SplitAndRemoveEmpty(',').Select(Guid.Parse).ToList();
        var list = GetNavLinksByIds(_ids);
        return _mapper.Map<List<NavLinkDto>>(list);
    }

    public NavLinkDto AddNavLink(CreateNavLinkInput input)
    {
        var oldLink = _crudService.GetOne<NavLink>(e => e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
        if (oldLink is not null)
        {
            _errorMessage = $"NavLink: TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }
        var NavLink = _mapper.Map<NavLink>(input);
        var createdNavLink = _crudService.Add<NavLink, Guid>(NavLink);
        _crudService.SaveChanges();
        return _mapper.Map<NavLinkDto>(createdNavLink);
    }
    public List<NavLinkDto> AddManyNavLinks(List<CreateNavLinkInput> inputs)
    {
        var titleArList = inputs.Select(e=>e.TitleAr).ToList();
        var titleEnList = inputs.Select(e=>e.TitleEn).ToList();
        var NavsExisted = _crudService.GetList<NavLink,Guid>(e => titleArList.Contains(e.TitleAr) || titleEnList.Contains(e.TitleEn));
        if (NavsExisted.Count != 0)
        {
            _errorMessage = $"NavLink: NavsLinks List Is rejected , Some TitleAr or TitleEn is already existed.";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
        }

        var NavLinks = _mapper.Map<List<NavLink>>(inputs);
        var createdNavLinks = _crudService.AddAndGetRange<NavLink, Guid>(NavLinks);
        _crudService.SaveChanges();
        return _mapper.Map<List<NavLinkDto>>(createdNavLinks);
    }

    public NavLinkDto UpdateNavLink(UpdateNavLinkInput input)
    {
        var oldNavLink = GetNavLinkById(input.Id);
        if (oldNavLink.TitleAr != input.TitleAr || oldNavLink.TitleEn != input.TitleEn)
        {
            var LinkExisted = _crudService.GetOne<NavLink>(e => e.TitleAr == input.TitleAr || e.TitleEn == input.TitleEn);
            if (LinkExisted is not null)
            {
                _errorMessage = $"NavLink: TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }

        var newNavLink = _mapper.Map<NavLink>(input);
        FillRestPropsWithOldValues(oldNavLink, newNavLink);
        var updatedNavLink = _crudService.Update<NavLink, Guid>(newNavLink);
        _crudService.SaveChanges();

        return _mapper.Map<NavLinkDto>(updatedNavLink);
    }
    public List<NavLinkDto> UpdateManyNavLinks(List<UpdateNavLinkInput> inputs)
    {
        var oldNavLinks = GetNavLinksByIds(inputs.Select(x => x.Id).ToList());
        var oldNavLinksTitlesAr = oldNavLinks.Where(m => !inputs.Select(e=>e.TitleAr).Contains(m.TitleAr)).Select(e => e.TitleAr).ToList();
        var oldNavLinksTitlesEn = oldNavLinks.Where(m => !inputs.Select(e=>e.TitleEn).Contains(m.TitleEn)).Select(e => e.TitleEn).ToList();
        if (oldNavLinksTitlesAr.Count != 0 || oldNavLinksTitlesEn.Count != 0) {
            var LinksExisted = _crudService.GetList<NavLink, Guid>(e => oldNavLinksTitlesAr.Contains(e.TitleAr) || oldNavLinksTitlesEn.Contains(e.TitleEn));
            if (LinksExisted.Count != 0)
            {
                _errorMessage = $"NavLink: Links List Is rejected , Some TitleAr or TitleEn is already existed.";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, HttpStatusCode.BadRequest);
            }
        }
        var newNavLinks = _mapper.Map<List<NavLink>>(inputs);
        for (int i = 0; i < oldNavLinks.Count; i++)
            FillRestPropsWithOldValues(oldNavLinks[i], newNavLinks[i]);
        var updatedNavLinks = _crudService.UpdateAndGetRange<NavLink, Guid>(newNavLinks);
        _crudService.SaveChanges();

        return _mapper.Map<List<NavLinkDto>>(updatedNavLinks);
    }
    
    public bool DeleteNavLink(Guid id)
    {
        var NavLink = GetNavLinkById(id);
        _crudService.SoftDelete<NavLink, Guid>(NavLink);
        _crudService.SaveChanges();
        return true;
    }
    public bool ActivateNavLink(Guid id)
    {
        var NavLink = GetNavLinkById(id);
        _crudService.Activate<NavLink, Guid>(NavLink);
        _crudService.SaveChanges();
        return true;
    }

}