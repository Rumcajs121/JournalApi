using Journal.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
namespace Journal.Application.Commons.Commands.UploadPicture;

public class UploadPictureCommand:IRequest<PictureDto>
{
    public IFormFile File { get; set; }
}