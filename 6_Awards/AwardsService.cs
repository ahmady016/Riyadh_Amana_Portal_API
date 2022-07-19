using amana_mono._6_Awards.Dtos;
using AutoMapper;
using Common;
using DB;
using DB.Common;
using DB.Entities;

namespace amana_mono._6_Awards
{
    public class AwardsService: IAwardsService
    {
        private readonly ICRUDService _crudService;
        private readonly IMapper _mapper;
        private readonly ILogger<Award> _logger;
        private string _errorMessage;

        public AwardsService(
            ICRUDService curdService,
            IMapper mapper,
            ILogger<Award> logger
        )
        {
            _crudService = curdService;
            _mapper = mapper;
            _logger = logger;
        }

        public List<AwardsDto> List(string type)
        {
            var list = type.ToLower() switch
            {
                "all" => _crudService.GetAll<Advertisement, Guid>(),
                "deleted" => _crudService.GetList<Advertisement, Guid>(e => e.IsDeleted),
                _ => _crudService.GetList<Advertisement, Guid>(e => !e.IsDeleted),
            };
            return _mapper.Map<List<AwardsDto>>(list);
        }
        public PageResult<AwardsDto> ListPage(string type, int pageSize, int pageNumber)
        {
            var query = type.ToLower() switch
            {
                "all" => _crudService.GetQuery<Advertisement>(),
                "deleted" => _crudService.GetQuery<Advertisement>(e => e.IsDeleted),
                _ => _crudService.GetQuery<Advertisement>(e => !e.IsDeleted),
            };
            var page = _crudService.GetPage(query, pageSize, pageNumber);
            return new PageResult<AwardsDto>()
            {
                PageItems = _mapper.Map<List<AwardsDto>>(page.PageItems),
                TotalItems = page.TotalItems,
                TotalPages = page.TotalPages
            };
        }

        public AwardsDto Find(Guid id)
        {
            var user = _crudService.Find<Advertisement, Guid>(id);
            if (user == null)
            {
                _errorMessage = $"Advertisement Record with Id: {id} Not Found";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
            }
            return _mapper.Map<AwardsDto>(user);
        }
        public List<AwardsDto> FindList(string ids)
        {
            if (ids == null)
            {
                _errorMessage = $"Advertisement: Must supply comma separated string of ids";
                _logger.LogError(_errorMessage);
                throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.BadRequest);
            }
            var _ids = ids.SplitAndRemoveEmpty(',').ToList();
            var list = _crudService.GetList<Advertisement, Guid>(e => _ids.Contains(e.Id.ToString()));
            return _mapper.Map<List<AwardsDto>>(list);
        }

        public AwardsDto Add(CreateAwardsInput input)
        {
            var advertisement = _mapper.Map<Advertisement>(input);
            var createdAdvertisement = _crudService.Add<Advertisement, Guid>(advertisement);
            _crudService.SaveChanges();
            return _mapper.Map<AwardsDto>(createdAdvertisement);
        }
        public List<AwardsDto> AddMany(List<CreateAwardsInput> inputs)
        {
            var advertisements = _mapper.Map<List<Advertisement>>(inputs);
            var createdAdvertisements = _crudService.AddAndGetRange<Advertisement, Guid>(advertisements);
            _crudService.SaveChanges();
            return _mapper.Map<List<AwardsDto>>(createdAdvertisements);
        }

        public AwardsDto Update(UpdateAwardsInput input)
        {
            var advertisement = _mapper.Map<Advertisement>(input);
            var updatedAdvertisement = _crudService.Update<Advertisement, Guid>(advertisement);
            _crudService.SaveChanges();
            return _mapper.Map<AwardsDto>(updatedAdvertisement);
        }
        public List<AwardsDto> UpdateMany(List<UpdateAwardsInput> inputs)
        {
            var advertisements = _mapper.Map<List<Advertisement>>(inputs);
            var updatedAdvertisements = _crudService.UpdateAndGetRange<Advertisement, Guid>(advertisements);
            _crudService.SaveChanges();
            return _mapper.Map<List<AwardsDto>>(updatedAdvertisements);
        }

        public bool Delete(Guid id)
        {
            var award = _crudService.Find<Award, Guid>(id);
            if (award is not null)
            {
                _crudService.SoftDelete<Award, Guid>(award);
                _crudService.SaveChanges();
                return true;
            }
            _errorMessage = $"Advertisement record Not Found!!!";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
        }
        public bool Activate(Guid id)
        {
            var award = _crudService.Find<Award, Guid>(id);
            if (award is not null)
            {
                _crudService.Activate<Award, Guid>(award);
                _crudService.SaveChanges();
                return true;
            }
            _errorMessage = $"Advertisement record Not Found!!!";
            _logger.LogError(_errorMessage);
            throw new HttpRequestException(_errorMessage, null, System.Net.HttpStatusCode.NotFound);
        }

    }
}
