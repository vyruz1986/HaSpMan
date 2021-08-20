using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Domain;
using Domain.Interfaces;

using MediatR;

namespace Commands.Handlers
{
    public class AddMemberHandler : IRequestHandler<AddMemberCommand, Guid>
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public AddMemberHandler(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(AddMemberCommand request, CancellationToken cancellationToken)
        {
            var newMember = _mapper.Map<Member>(request);
            _memberRepository.Add(newMember);
            await _memberRepository.Save();
            return newMember.Id;
        }
    }
}