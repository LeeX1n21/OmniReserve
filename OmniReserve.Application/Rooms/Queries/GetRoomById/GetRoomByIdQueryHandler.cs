using FluentValidation;

namespace OmniReserve.Application.Rooms.Queries.GetRoomById;

public class GetRoomByIdQueryValidator : AbstractValidator<GetRoomByIdQuery>
{
    public GetRoomByIdQueryValidator()
    {
        RuleFor(x => x.RoomId)
            .NotEmpty().WithMessage("El identificador de la habitación es obligatorio.");
    }
}