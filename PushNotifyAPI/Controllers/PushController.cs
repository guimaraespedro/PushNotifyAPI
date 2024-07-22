using Microsoft.AspNetCore.Mvc;
using PushNotifyAPI.DTOS;
using PushNotifyAPI.Entities;
using PushNotifyAPI.Repositories;
using PushNotifyAPI.Services;

namespace PushNotifyAPI.Controllers
{
    [ApiController]
    [Route("push")]
    public class PushController : ControllerBase
    {
        private readonly IPushService _pushService;
        private readonly IUserDeviceInfoRepository _userDeviceInfoRepository;
        private readonly INotificationRepository _notificationRepository;

        public PushController(IPushService pushService, IUserDeviceInfoRepository userDeviceInfoRepository, INotificationRepository notificationRepository)
        {
            _pushService = pushService;
            _userDeviceInfoRepository = userDeviceInfoRepository;
            _notificationRepository = notificationRepository;
        }

        [HttpPost]
        [Route("send")]
        public async Task<ActionResult> SendNotification([FromBody] NotificationDTO notificationDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            UserDeviceInfo? receiverDeviceInfo = await _userDeviceInfoRepository.GetAsyncFromUserId(notificationDTO.ReceiverUserId);
            if (receiverDeviceInfo == null)
            {
                return NotFound();
            }

            try
            {
                await _pushService.SendNotificationAsync(receiverDeviceInfo.DeviceToken, notificationDTO);
            }
            catch (Exception e)
            {
                return Ok(e);
            }


            return Ok();
        }
        [HttpPost("create-notif")]
        public async Task<ActionResult> CreateNotification(NotificationDTO notificationDTO)
        {

            Entities.Notification notification = new()
            {
                Content = notificationDTO.Content,
                ReceiverDeviceToken = "Token",
                ReceiverId = notificationDTO.ReceiverUserId,
                SendAt = DateTime.Now,
                SenderId = notificationDTO.SenderUserId
            };

            await _notificationRepository.CreateAsync(notification);
            return Ok();
        }

        [HttpPost]
        [Route("register-to-notification")]
        public async Task<ActionResult> RegisterToNotification([FromBody] UserDeviceInfo userDeviceInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            UserDeviceInfo? deviceInfo = await _userDeviceInfoRepository.GetAsync(userDeviceInfo.DeviceToken);
            if (deviceInfo != null)
            {
                return Ok("Already registered");
            }
            await _userDeviceInfoRepository.CreateAsync(userDeviceInfo);
            return Ok();
        }

        [HttpGet]
        [Route("get-notifications")]
        public async Task<ActionResult> GetNotifications(string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            ICollection<Notification> notifications = await _notificationRepository.GetAsync(userId);
            return Ok(notifications);
        }

        [HttpGet]
        [Route("get-user-device-info")]
        public async Task<ActionResult> GetUserDeviceInfo(string userId)
        {
            UserDeviceInfo? deviceInfo = await _userDeviceInfoRepository.GetAsyncFromUserId(userId);
            return Ok(deviceInfo);

        }
    }
}