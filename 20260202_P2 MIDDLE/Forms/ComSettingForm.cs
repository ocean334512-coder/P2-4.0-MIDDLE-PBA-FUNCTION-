using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace _20260202_P2_MIDDLE.Forms
{
    public partial class ComSettingForm : Form
    {
        // CH1 통신용 TCP 클라이언트
        private TcpClient _tcpCh1Client;
        // Jig용 시리얼 포트
        private SerialPort _jigSerialPort;
        // LCR용 시리얼 포트
        private SerialPort _lcrSerialPort;

        public ComSettingForm()
        {
            InitializeComponent();

            // TCP CH1 기본값 세팅
            tboxTcpCh1Port.Text = "192.168.0.101";
            tboxTcpCh1Ip.Text = "5000";

            // 폼이 생성될 때 COM 포트 / 보레이트 목록 초기화
            InitializeSerialControls();

            // 상태 라벨 초기화 (미연결 상태)
            SetLabelDisconnected(EternetComeState);
            SetLabelDisconnected(JigComeState);
            SetLabelDisconnected(LcrComeState);
        }

        /// <summary>
        /// 라벨을 "Connected" (초록) 상태로 변경
        /// </summary>
        private void SetLabelConnected(Label lbl)
        {
            lbl.Text = "Connected";
            lbl.BackColor = Color.LimeGreen;
            lbl.ForeColor = Color.White;
        }

        /// <summary>
        /// 라벨을 "Not Connect" (적색) 상태로 변경
        /// </summary>
        private void SetLabelDisconnected(Label lbl)
        {
            lbl.Text = "Not Connect";
            lbl.BackColor = Color.Red;
            lbl.ForeColor = Color.White;
        }

        /// <summary>
        /// Jig / LCR COM 포트 및 보레이트 콤보박스 초기화
        /// </summary>
        private void InitializeSerialControls()
        {
            // 사용 가능한 COM 포트 목록
            try
            {
                var ports = SerialPort.GetPortNames()
                                      .OrderBy(p => p)
                                      .ToArray();

                cboxJigPort.Items.Clear();
                cboxJigPort.Items.AddRange(ports);
                if (ports.Length > 0)
                    cboxJigPort.SelectedIndex = 0;

                cboxLcrPort.Items.Clear();
                cboxLcrPort.Items.AddRange(ports);
                if (ports.Length > 0)
                    cboxLcrPort.SelectedIndex = 0;
            }
            catch { }

            // 보레이트 목록
            int[] baudRates = { 9600, 19200, 38400, 57600, 115200, 230400 };

            cboxJigBaudRate.Items.Clear();
            cboxLcrBaudRate.Items.Clear();
            foreach (int br in baudRates)
            {
                cboxJigBaudRate.Items.Add(br.ToString());
                cboxLcrBaudRate.Items.Add(br.ToString());
            }

            // 기본값 9600
            cboxJigBaudRate.SelectedItem = "9600";
            cboxLcrBaudRate.SelectedItem = "9600";
        }

        private void tboxTcpCh1Port_TextChanged(object sender, EventArgs e)
        {
        }

        private void tboxTcpCh1Ip_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// btnConnect 클릭 시:
        /// 1) CH1 TCP 통신 연결
        /// 2) Jig 시리얼 통신 연결
        /// 3) LCR 시리얼 통신 연결
        /// </summary>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            // ==================== 1) TCP CH1 연결 ====================
            string ipText = tboxTcpCh1Port.Text.Trim();
            string portText = tboxTcpCh1Ip.Text.Trim();

            if (!IPAddress.TryParse(ipText, out _))
            {
                MessageBox.Show("유효한 IP 주소를 입력해 주세요.", "입력 오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tboxTcpCh1Port.Focus();
                return;
            }

            if (!int.TryParse(portText, out int port) || port <= 0 || port > 65535)
            {
                MessageBox.Show("유효한 Port 번호(1~65535)를 입력해 주세요.", "입력 오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tboxTcpCh1Ip.Focus();
                return;
            }

            try
            {
                if (_tcpCh1Client != null)
                {
                    if (_tcpCh1Client.Connected)
                        _tcpCh1Client.Close();
                    _tcpCh1Client = null;
                }

                _tcpCh1Client = new TcpClient();
                _tcpCh1Client.Connect(ipText, port);

                SetLabelConnected(EternetComeState);
            }
            catch (Exception ex)
            {
                SetLabelDisconnected(EternetComeState);
                MessageBox.Show($"TCP 통신 연결에 실패했습니다.\n\n{ex.Message}", "연결 실패",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (_tcpCh1Client != null)
                {
                    _tcpCh1Client.Close();
                    _tcpCh1Client = null;
                }
            }

            // ==================== 2) Jig 시리얼 통신 연결 ====================
            string jigPortName = cboxJigPort.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(jigPortName))
            {
                MessageBox.Show("Jig용 COM 포트를 선택해 주세요.", "입력 오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string jigBaudText = cboxJigBaudRate.SelectedItem?.ToString() ?? cboxJigBaudRate.Text.Trim();
            if (!int.TryParse(jigBaudText, out int jigBaud) || jigBaud <= 0)
            {
                MessageBox.Show("유효한 Jig Baud Rate를 선택해 주세요.", "입력 오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_jigSerialPort != null)
                {
                    if (_jigSerialPort.IsOpen)
                        _jigSerialPort.Close();
                    _jigSerialPort.Dispose();
                    _jigSerialPort = null;
                }

                _jigSerialPort = new SerialPort(jigPortName, jigBaud, Parity.None, 8, StopBits.One);
                _jigSerialPort.Open();

                SetLabelConnected(JigComeState);
            }
            catch (Exception ex)
            {
                SetLabelDisconnected(JigComeState);
                MessageBox.Show($"Jig 시리얼 통신 연결에 실패했습니다.\n\n{ex.Message}", "연결 실패",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (_jigSerialPort != null)
                {
                    try { if (_jigSerialPort.IsOpen) _jigSerialPort.Close(); } catch { }
                    _jigSerialPort.Dispose();
                    _jigSerialPort = null;
                }
            }

            // ==================== 3) LCR 시리얼 통신 연결 ====================
            string lcrPortName = cboxLcrPort.SelectedItem as string;
            if (string.IsNullOrWhiteSpace(lcrPortName))
            {
                MessageBox.Show("LCR용 COM 포트를 선택해 주세요.", "입력 오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string lcrBaudText = cboxLcrBaudRate.SelectedItem?.ToString() ?? cboxLcrBaudRate.Text.Trim();
            if (!int.TryParse(lcrBaudText, out int lcrBaud) || lcrBaud <= 0)
            {
                MessageBox.Show("유효한 LCR Baud Rate를 선택해 주세요.", "입력 오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_lcrSerialPort != null)
                {
                    if (_lcrSerialPort.IsOpen)
                        _lcrSerialPort.Close();
                    _lcrSerialPort.Dispose();
                    _lcrSerialPort = null;
                }

                _lcrSerialPort = new SerialPort(lcrPortName, lcrBaud, Parity.None, 8, StopBits.One);
                _lcrSerialPort.Open();

                SetLabelConnected(LcrComeState);
            }
            catch (Exception ex)
            {
                SetLabelDisconnected(LcrComeState);
                MessageBox.Show($"LCR 시리얼 통신 연결에 실패했습니다.\n\n{ex.Message}", "연결 실패",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (_lcrSerialPort != null)
                {
                    try { if (_lcrSerialPort.IsOpen) _lcrSerialPort.Close(); } catch { }
                    _lcrSerialPort.Dispose();
                    _lcrSerialPort = null;
                }
            }
        }
    }
}
