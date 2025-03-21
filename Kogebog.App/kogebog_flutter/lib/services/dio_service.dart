import 'package:dio/dio.dart';

class DioService {
  static final Dio _dioJson = Dio(BaseOptions(
    baseUrl: 'http://10.0.2.2:5023/api',
    headers: {
      "Content-Type": "application/json",
      'Accept': 'application/json'
    },
  ));

  static Dio get dioJson => _dioJson;

  static final Dio _dioMultipart = Dio(BaseOptions(
    baseUrl: 'http://10.0.2.2:5023/api',
    headers: {
      "Content-Type": "multipart/form-data",
      'Accept': 'application/json'
    },
  ));

  static Dio get dioMultipart => _dioMultipart;
}
