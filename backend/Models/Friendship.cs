using System.ComponentModel.DataAnnotations;

namespace GigaChat.Models;

public class FriendShip
{
    [Key]
    [Required]
    public Guid Id { get; set; }

    [Required]
    public User Proposer { get; set; } = null!;

    [Required]
    public User Accepter { get; set; } = null!;

    [Required]
    public bool IsAccepted { get; set; } = false;

    [Required]
    public DateTime DateOfProposal { get; set; }

    public DateTime DateOfAcceptance { get; set; }
}
