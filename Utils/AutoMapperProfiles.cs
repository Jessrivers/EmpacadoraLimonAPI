using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EmpacadoraLimonAPI.DTOs;
using EmpacadoraLimonAPI.Models;

namespace EmpacadoraLimonAPI.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Proveedor
            CreateMap<ProveedorDTOCrear, Proveedor>();
            CreateMap<Proveedor, ProveedorDTO>().ReverseMap();

            // Destino
            CreateMap<DestinoDTOCrear, Destino>();
            CreateMap<Destino, DestinoDTO>().ReverseMap();

            // Lote
            CreateMap<LoteDTOCrear, Lote>();
            CreateMap<Lote, LoteDTO>()
                .ForMember(dest => dest.NombreProveedor,
                    opt => opt.MapFrom(src => src.IdProveedorNavigation!.Nombre));

            // Movimiento
            CreateMap<MovimientoDTOCrear, Movimiento>();
            CreateMap<Movimiento, MovimientoDTO>()
                .ForMember(dest => dest.NombreDestino,
                    opt => opt.MapFrom(src => src.IdDestinoNavigation!.Nombre));
        }
    }
}
