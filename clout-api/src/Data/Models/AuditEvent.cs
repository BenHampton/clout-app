
using AutoMapper;
using clout_api.Data.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace clout_api.Data.Models;

public class AuditEvent : AuditRecordBase
{
    public int Id { get; set; }

    public int PipelineStationId { get; set; }

    public int JointBatchId { get; set; }

    public int EventTypeId { get; set; }

    public int ScheduleVersionId { get; set; }

    public DateTimeOffset StartedAt { get; set; }

    public DateTimeOffset FinishedAt { get; set; }

    public int RateIntoStation { get; set; }

    public int EventRate { get; set; }

    public int RateOutOfStation { get; set; }

    public int Volume { get; set; }

    protected override Action<ModelBuilder> OnBuildAction => (modelBuilder) =>
        modelBuilder.Entity<AuditEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("audit_event_pk");

            entity.ToTable("audit_event");

            entity.Property(e => e.Id)
                .HasColumnName("id");


            entity.Property(e => e.EventRate).HasColumnName("event_rate");
            entity.Property(e => e.FinishedAt)
                            .HasColumnType("timestamp with time zone")
                            .HasColumnName("finished_at");
            entity.Property(e => e.JointBatchId).HasColumnName("joint_batch_id");
            entity.Property(e => e.RateIntoStation).HasColumnName("rate_into_station");
            entity.Property(e => e.StartedAt)
                            .HasColumnType("timestamp with time zone")
                            .HasColumnName("started_at");
            entity.Property(e => e.PipelineStationId).HasColumnName("pipeline_station_id");
            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.ScheduleVersionId).HasColumnName("schedule_version_id");
            entity.Property(e => e.Volume).HasColumnName("volume");

            BuildAuditBaseEntity(entity);
            BuildModelBaseEntity(entity);
        });
}
