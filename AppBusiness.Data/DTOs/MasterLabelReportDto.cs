namespace AppBusiness.Data.DTOs
{
    public class MasterLabelReportDto : MasterLabelReportCetakDto
    {
        public int? Id { get; set; }
        public int? IdPdam { get; set; }
        public string Key { get; set; }
    }

    public class MasterLabelReportCetakDto
    {
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Keterangan { get; set; }
        public string ParafJabatan1 { get; set; }
        public string ParafNama1 { get; set; }
        public string ParafKeterangan1 { get; set; }
        public string ParafId1 { get; set; }
        public string ParafJabatan2 { get; set; }
        public string ParafNama2 { get; set; }
        public string ParafKeterangan2 { get; set; }
        public string ParafId2 { get; set; }
        public string ParafJabatan3 { get; set; }
        public string ParafNama3 { get; set; }
        public string ParafKeterangan3 { get; set; }
        public string ParafId3 { get; set; }
        public string ParafJabatan4 { get; set; }
        public string ParafNama4 { get; set; }
        public string ParafKeterangan4 { get; set; }
        public string ParafId4 { get; set; }
        public string Footer1 { get; set; }
        public string Footer2 { get; set; }
    }

    public class ParamFilterLabelReportDto
    {
        public int? IdPdam { get; set; } = 0;
        public string Key { get; set; }
    }
}
