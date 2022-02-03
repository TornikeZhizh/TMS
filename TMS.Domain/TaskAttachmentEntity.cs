
namespace TMS.Domain
{
    public class TaskAttachmentEntity
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public TaskEntity Task { get; set; }
        public string FileUid { get; set; }
        public string FileName { get; set; }
    }
}
