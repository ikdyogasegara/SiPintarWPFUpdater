using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppBusiness.Data.DTOs
{
    public class MasterPelangganGlobalKoreksiWpf
    {
        public int? IdPdam { get; set; }
        public int? IdKoreksi { get; set; }
        public string SumberPerubahan { get; set; }
        public DateTime? WaktuKoreksi { get; set; }
        public int? IdUser { get; set; }
        public string NamaUser { get; set; }
        public int? IdPelangganAir { get; set; }
        public string NoSamb { get; set; }
        public string NoSamb_Baru { get; set; }
        public int? IdPelangganLimbah { get; set; }
        public string NomorLimbah { get; set; }
        public string NomorLimbah_Baru { get; set; }
        public int? IdPelangganLltt { get; set; }
        public string NomorLltt { get; set; }
        public string NomorLltt_Baru { get; set; }
        public string Nama { get; set; }
        public string Nama_Baru { get; set; }
        public string Alamat { get; set; }
        public string Alamat_Baru { get; set; }
        public string Rt { get; set; }
        public string Rt_Baru { get; set; }
        public string Rw { get; set; }
        public string Rw_Baru { get; set; }
        public int? IdGolongan { get; set; }
        public string KodeGolongan { get; set; }
        public string NamaGolongan { get; set; }
        public int? IdGolongan_Baru { get; set; }
        public string KodeGolongan_Baru { get; set; }
        public string NamaGolongan_Baru { get; set; }
        public int? IdDiameter { get; set; }
        public string KodeDiameter { get; set; }
        public string NamaDiameter { get; set; }
        public int? IdDiameter_Baru { get; set; }
        public string KodeDiameter_Baru { get; set; }
        public string NamaDiameter_Baru { get; set; }

        public int? IdTarifLimbah { get; set; }
        public string KodeTarifLimbah { get; set; }
        public string NamaTarifLimbah { get; set; }
        public int? IdTarifLimbah_Baru { get; set; }
        public string KodeTarifLimbah_Baru { get; set; }
        public string NamaTarifLimbah_Baru { get; set; }

        public int? IdTarifLltt { get; set; }
        public string KodeTarifLltt { get; set; }
        public string NamaTarifLltt { get; set; }
        public int? IdTarifLltt_Baru { get; set; }
        public string KodeTarifLltt_Baru { get; set; }
        public string NamaTarifLltt_Baru { get; set; }


        public int? IdRayon { get; set; }
        public string KodeRayon { get; set; }
        public string NamaRayon { get; set; }
        public int? IdRayon_Baru { get; set; }
        public string KodeRayon_Baru { get; set; }
        public string NamaRayon_Baru { get; set; }
        public int? IdKelurahan { get; set; }
        public string KodeKelurahan { get; set; }
        public string NamaKelurahan { get; set; }
        public int? IdKelurahan_Baru { get; set; }
        public string KodeKelurahan_Baru { get; set; }
        public string NamaKelurahan_Baru { get; set; }
        public int? IdKolektif { get; set; }
        public string KodeKolektif { get; set; }
        public string NamaKolektif { get; set; }
        public int? IdKolektif_Baru { get; set; }
        public string KodeKolektif_Baru { get; set; }
        public string NamaKolektif_Baru { get; set; }
        public int? IdStatus { get; set; }
        public string NamaStatus { get; set; }
        public int? IdStatus_Baru { get; set; }
        public string NamaStatus_Baru { get; set; }
        public int? IdFlag { get; set; }
        public string NamaFlag { get; set; }
        public int? IdFlag_Baru { get; set; }
        public string NamaFlag_Baru { get; set; }

        public string NoHp { get; set; }
        public string NoHp_Baru { get; set; }
        public string NoTelp { get; set; }
        public string NoTelp_Baru { get; set; }
        public string NoKtp { get; set; }
        public string NoKtp_Baru { get; set; }
        public string Email { get; set; }
        public string Email_Baru { get; set; }

        public string Latitude { get; set; }
        public string Latitude_Baru { get; set; }
        public string Longitude { get; set; }
        public string Longitude_Baru { get; set; }
        public DateTime? WaktuUpdate { get; set; }
        public int? IdUserRequest { get; set; }


        public bool FlagNoSambWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(NoSamb_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagNomorLimbahWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(NomorLimbah_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagNomorLlttWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(NomorLltt_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagNamaWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Nama_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagAlamatWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Alamat_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagRtWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Rt_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagRwWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Rw_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagGolonganWpf
        {
            get
            {
                if (IdGolongan_Baru != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagDiameterWpf
        {
            get
            {
                if (IdDiameter_Baru != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagTarifLimbahWpf
        {
            get
            {
                if (IdTarifLimbah_Baru != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagTarifLlttWpf
        {
            get
            {
                if (IdTarifLltt_Baru != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagRayonWpf
        {
            get
            {
                if (IdRayon_Baru != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagKelurahanWpf
        {
            get
            {
                if (IdKelurahan_Baru != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagKolektifWpf
        {
            get
            {
                if (IdKolektif_Baru != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagStatusWpf
        {
            get
            {
                if (IdStatus_Baru != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagFlagWpf
        {
            get
            {
                if (IdFlag_Baru != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagNoHpWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(NoHp_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagNoTelpWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(NoTelp_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagNoKtpWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(NoKtp_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagEmailWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Email_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagLatitudeWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Latitude_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool FlagLongitudeWpf
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Longitude_Baru))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
